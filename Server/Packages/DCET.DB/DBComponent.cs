using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DCET
{
	[ObjectSystem]
	public class DBComponentAwakeSystem : AwakeSystem<DBComponent, DBConfig>
	{
		public override void Awake(DBComponent self, DBConfig dbConfig)
		{
			self.Awake(dbConfig);
		}
	}

	[ObjectSystem]
	public class DBComponentDestroySystem : DestroySystem<DBComponent>
	{
		public override void Destroy(DBComponent self)
		{
			self.Destory();
		}
	}

	/// <summary>
	/// 用来缓存数据
	/// </summary>
	public class DBComponent : Entity
	{
		public DBComponent Instance;
		
		public List<string> Transfers = new List<string>();
		
		public const int TaskCount = 32;
		
		public MongoClient mongoClient;
		public IMongoDatabase database;


		public void Awake(DBConfig dbConfig)
		{
			string connectionString = dbConfig.ConnectionString;
			mongoClient = new MongoClient(connectionString);
			database = mongoClient.GetDatabase(dbConfig.DBName);

			Transfers.Clear();
			foreach (Type type in Game.EventSystem.GetAllType())
			{
				if (type == typeof(IDBCollection))
				{
					continue;
				}
				if (!typeof(IDBCollection).IsAssignableFrom(type))
				{
					continue;
				}
				Transfers.Add(type.Name);
			}

			Instance = this;
		}

		public void Destory()
		{
			Instance = null;
			Transfers.Clear();
		}


		public IMongoCollection<T> GetCollection<T>(string collection=null)
		{
			return this.database.GetCollection<T>(collection ?? typeof (T).Name);
		} 
		
		public IMongoCollection<Entity> GetCollection(string name)
		{
			return this.database.GetCollection<Entity>(name);
		}

		#region Query

		public async Task<T> Query<T>(long id, string collection = null) where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
			{
				IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(d => d.Id == id);

				return await cursor.FirstOrDefaultAsync();
			}
		}

		public async Task<List<T>> Query<T>(Expression<Func<T, bool>> filter, string collection = null)
				where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
			{
				IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filter);

				return await cursor.ToListAsync();
			}
		}

		public async Task<List<T>> Query<T>(long taskId, Expression<Func<T, bool>> filter, string collection = null)
				where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
			{
				IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filter);

				return await cursor.ToListAsync();
			}
		}

		public async Task Query(long id, List<string> collectionNames, List<Entity> result)
		{
			if (collectionNames == null || collectionNames.Count == 0)
			{
				return;
			}

			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
			{
				foreach (string collectionName in collectionNames)
				{
					IAsyncCursor<Entity> cursor = await GetCollection(collectionName).FindAsync(d => d.Id == id);

					Entity e = await cursor.FirstOrDefaultAsync();

					if (e == null)
					{
						continue;
					}

					result.Add(e);
				}
			}
		}

		public async Task<List<T>> QueryJson<T>(string json, string collection = null) where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
			{
				FilterDefinition<T> filterDefinition = new JsonFilterDefinition<T>(json);
				IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filterDefinition);
				return await cursor.ToListAsync();
			}
		}

		public async Task<List<T>> QueryJson<T>(long taskId, string json, string collection = null) where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
			{
				FilterDefinition<T> filterDefinition = new JsonFilterDefinition<T>(json);
				IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filterDefinition);
				return await cursor.ToListAsync();
			}
		}

		#endregion

		#region Insert

		public async Task InsertBatch<T>(IEnumerable<T> list, string collection = null) where T : Entity
		{
			if (collection == null)
			{
				collection = typeof(T).Name;
			}

			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
			{
				await GetCollection(collection).InsertManyAsync(list);
			}
		}

		#endregion

		#region Save

		public async Task Save<T>(T entity, string collection = null) where T : Entity
		{
			if (entity == null)
			{
				Log.Error($"save entity is null: {typeof(T).Name}");

				return;
			}

			if (collection == null)
			{
				collection = entity.GetType().Name;
			}

			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, entity.Id % DBComponent.TaskCount))
			{
				await GetCollection(collection).ReplaceOneAsync(d => d.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
			}
		}

		public async Task Save<T>(long taskId, T entity, string collection = null) where T : Entity
		{
			if (entity == null)
			{
				Log.Error($"save entity is null: {typeof(T).Name}");

				return;
			}

			if (collection == null)
			{
				collection = entity.GetType().Name;
			}

			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
			{
				await GetCollection(collection).ReplaceOneAsync(d => d.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
			}
		}

		public async Task Save(long id, List<Entity> entities)
		{
			if (entities == null)
			{
				Log.Error($"save entity is null");
				return;
			}

			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
			{
				foreach (Entity entity in entities)
				{
					if (entity == null)
					{
						continue;
					}

					await GetCollection(entity.GetType().Name)
							.ReplaceOneAsync(d => d.Id == entity.Id, entity, new UpdateOptions { IsUpsert = true });
				}
			}
		}

		public async void SaveNotWait<T>(T entity, long taskId = 0, string collection = null) where T : Entity
		{
			if (taskId == 0)
			{
				await Save(entity, collection);

				return;
			}

			await Save(taskId, entity, collection);
		}

		#endregion

		#region Remove

		public async Task<long> Remove<T>(long id, string collection = null) where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
			{
				DeleteResult result = await GetCollection<T>(collection).DeleteOneAsync(d => d.Id == id);

				return result.DeletedCount;
			}
		}

		public async Task<long> Remove<T>(long taskId, long id, string collection = null) where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
			{
				DeleteResult result = await GetCollection<T>(collection).DeleteOneAsync(d => d.Id == id);

				return result.DeletedCount;
			}
		}

		public async Task<long> Remove<T>(Expression<Func<T, bool>> filter, string collection = null) where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
			{
				DeleteResult result = await GetCollection<T>(collection).DeleteManyAsync(filter);

				return result.DeletedCount;
			}
		}

		public async Task<long> Remove<T>(long taskId, Expression<Func<T, bool>> filter, string collection = null)
				where T : Entity
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
			{
				DeleteResult result = await GetCollection<T>(collection).DeleteManyAsync(filter);

				return result.DeletedCount;
			}
		}
		#endregion
	}
}
