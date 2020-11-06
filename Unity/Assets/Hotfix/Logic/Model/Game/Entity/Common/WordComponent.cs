using ET;
using FlatBuffers;
using Cal.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET
{
    
    public class WordComponentAwakeSystem : AwakeSystem<WordComponent>
    {
        public override void Awake(WordComponent self)
        {
            self.Awake();
        }
    }

    public class WordComponent:Entity
    {
        private string[] forbiddenStrArr;

        internal void Awake()
        {
            IFlatbufferObject[] forbiddenArr = DataTableHelper.GetAll<Forbidden>();
            forbiddenStrArr = new string[forbiddenArr.Length];
            int index = 0;
            foreach (Forbidden item in forbiddenArr)
            {
                forbiddenStrArr[index++] = item.Words;
            }
            IllegalWordHelper.Init(forbiddenStrArr);
        }
    }
}
