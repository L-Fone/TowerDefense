﻿//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading;
//using ET;

//namespace ET
//{

//    public class SessionAwakeSystem : AwakeSystem<Session, ET.Session>
//    {
//        public override void Awake(Session self, ET.Session session)
//        {
//            self.session = session;
//            SessionCallbackComponent sessionComponent = self.session.AddComponent<SessionCallbackComponent>();
//            sessionComponent.MessageCallback = (s, opcode, memoryStream) => { self.Run(s, opcode, memoryStream); };
//            sessionComponent.DisposeCallback = s => { self.Dispose(); };
//        }
//    }

//    /// <summary>
//    /// 用来收发热更层的消息
//    /// </summary>
//    public class Session : Entity
//    {
//        public ET.Session session;

//        private static int RpcId { get; set; }
//        private readonly Dictionary<int, Action<IResponse>> requestCallback = new Dictionary<int, Action<IResponse>>();

//        public override void Dispose()
//        {
//            if (this.IsDisposed)
//            {
//                return;
//            }

//            base.Dispose();

//            foreach (Action<IResponse> action in this.requestCallback.Values.ToArray())
//            {
//                action.Invoke(new ErrorResponse { Error = this.session.Error });
//            }

//            this.requestCallback.Clear();

//            this.session.Dispose();
//        }

//        public void Run(ET.Session s, ushort opcode, MemoryStream memoryStream)
//        {
//            OpcodeTypeComponent opcodeTypeComponent = Game.Scene.GetComponent<OpcodeTypeComponent>();
//            object instance = opcodeTypeComponent.GetInstance(opcode);
//            object message = this.session.Network.MessagePacker.DeserializeFrom(instance, memoryStream);

//            if (OpcodeHelper.IsNeedDebugLogMessage(opcode))
//            {
//                Log.Msg(opcode, message);
//            }

//            IResponse response = message as IResponse;
//            if (response == null)
//            {
//                Game.Scene.GetComponent<MessageDispatcherComponent>().Handle(session, new MessageInfo(opcode, message));
//                return;
//            }

//            Action<IResponse> action;
//            if (!this.requestCallback.TryGetValue(response.RpcId, out action))
//            {
//                throw new Exception($"not found rpc, response message: {StringHelper.MessageToStr(response)}");
//            }
//            this.requestCallback.Remove(response.RpcId);

//            action(response);
//        }

//        public void Send(IMessage message)
//        {
//            ushort opcode = Game.Scene.GetComponent<OpcodeTypeComponent>().GetOpcode(message.GetType());
//            this.Send(opcode, message);
//        }

//        public void Send(ushort opcode, IMessage message)
//        {
//            if (OpcodeHelper.IsNeedDebugLogMessage(opcode))
//            {

//                Log.Msg(opcode, message);
//            }
//            session.Send(opcode, message);
//        }

//        public ETTask<IResponse> Call(IRequest request)
//        {
//            int rpcId = ++RpcId;
//            var tcs = new ETTaskCompletionSource<IResponse>();

//            this.requestCallback[rpcId] = (response) =>
//            {
//                try
//                {
//                    if (ErrorCode.IsRpcNeedThrowException(response.Error))
//                    {
//                        throw new RpcException(response.Error, response.Message);
//                    }

//                    tcs.SetResult(response);
//                }
//                catch (Exception e)
//                {
//                    tcs.SetException(new Exception($"Rpc Error: {request.GetType().FullName}", e));
//                }
//            };

//            request.RpcId = rpcId;

//            this.Send(request);
//            return tcs.Task;
//        }

//        public ETTask<IResponse> Call(IRequest request, CancellationToken cancellationToken)
//        {
//            int rpcId = ++RpcId;
//            var tcs = new ETTaskCompletionSource<IResponse>();

//            this.requestCallback[rpcId] = (response) =>
//            {
//                try
//                {
//                    if (ErrorCode.IsRpcNeedThrowException(response.Error))
//                    {
//                        throw new RpcException(response.Error, response.Message);
//                    }

//                    tcs.SetResult(response);
//                }
//                catch (Exception e)
//                {
//                    tcs.SetException(new Exception($"Rpc Error: {request.GetType().FullName}", e));
//                }
//            };

//            cancellationToken.Register(() => { this.requestCallback.Remove(rpcId); });

//            request.RpcId = rpcId;

//            this.Send(request);
//            return tcs.Task;
//        }
//    }
//}
