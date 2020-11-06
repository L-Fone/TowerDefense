using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ET
{
    public class UserData:Entity,ISerializeToEntity
    {
        public List<int> unlockedTowerIdList = new List<int>();

        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float damageMulti = 1;

        public int money;
    }
}
