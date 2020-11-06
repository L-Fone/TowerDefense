using ET;
using UnityEngine;

namespace libx
{
    public interface INetworkMonitorListener
    {
        void OnReachablityChanged(NetworkReachability reachability);
    }

    public class NetworkMonitorAwakeSystem : AwakeSystem<NetworkMonitor>
    {
        public override void Awake(NetworkMonitor self)
        {
            self.Awake();
        }
    }
    public class NetworkMonitorUpdateSystem : UpdateSystem<NetworkMonitor>
    {
        public override void Update(NetworkMonitor self)
        {
            self.Update();
        }
    }

    public class NetworkMonitor : Entity
    {
        private NetworkReachability _reachability;
        public INetworkMonitorListener listener { get; set; }
        [SerializeField]private float sampleTime = 0.5f;
        private float _time;
        private bool _started;
        
        public void Awake()
        {
            _reachability = Application.internetReachability;
            Restart();
        }

        public void Restart()
        {
            _time = TimeHelper.ClientNowSeconds();
            _started = true;
        }

        public void Stop()
        {
            _started = false;
        }

        public void Update()
        {
            var now = TimeHelper.ClientNowSeconds();
            if (_started && now - _time >= sampleTime)
            {
                var state = Application.internetReachability;
                if (_reachability != state)
                {
                    if (listener != null)
                    {
                        listener.OnReachablityChanged(state);
                    }
                    _reachability = state;
                } 
                _time =now;  
            } 
        }
    }
}