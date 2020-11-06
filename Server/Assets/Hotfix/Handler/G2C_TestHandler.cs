using System.Threading.Tasks;

namespace DCET
{
    [MessageHandler]
    public class G2C_TestHandler: AMHandler<G2C_Test>
    {
        public static int count = 0;
        protected override async Task Run(Session session, G2C_Test message)
        {
            count++;
            await Task.CompletedTask;
        }
    }
}