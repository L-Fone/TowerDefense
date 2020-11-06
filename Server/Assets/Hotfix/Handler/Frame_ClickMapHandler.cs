using System.Threading.Tasks;
using UnityEngine;

namespace DCET
{
	[ActorMessageHandler]
	public class Frame_ClickMapHandler : AMActorLocationHandler<Unit, Frame_ClickMap>
	{
		protected override async Task Run(Unit unit, Frame_ClickMap message)
		{
			Vector3 target = new Vector3(message.X, message.Y, message.Z);
			unit.GetComponent<UnitPathComponent>().MoveTo(target);
			await Task.CompletedTask;
		}
	}
}