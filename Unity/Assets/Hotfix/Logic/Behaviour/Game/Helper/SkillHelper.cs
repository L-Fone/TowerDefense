using Cal;
using Cal.DataTable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ET
{
    public class SkillHelper
    {
        public static byte[] GetSkillConfig()
        {
            string path = "../Config/Skill/SkillConfig.bytes";
            return File.ReadAllBytes(path);
        }
        public static byte[] GetSkillBuffConfig()
        {
            string path = "../Config/Skill/BuffConfig.bytes";
            return File.ReadAllBytes(path);
        }
        public static byte[] GetSkillLogicConfig()
        {
            string path = "../Config/Skill/SkillLogicConfig.bytes";
            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// 从一个迭代器中获取指定条件，指定数量的元素，返回数组类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源数据</param>
        /// <param name="list">临时列表</param>
        /// <param name="indexList">临时索引列表</param>
        /// <param name="match">条件表达式</param>
        /// <param name="targetCount">需要的数量</param>
        /// <returns></returns>
        public static T[] SelectTarget<T>(IEnumerable<T> source, List<T> list, List<int> indexList, Func<T, bool> match, int targetCount, T firstT = null)
            where T : class
        {
            if (firstT != null)
            {
                targetCount--;
            }
            T[] ret = new T[targetCount];

            if (source == null) return null;
            list.Clear();
            foreach (var item in source)
            {
                if (item != firstT)
                    if (match(item))
                    {
                        list.Add(item);
                    }
            }

            var sourceCount = list.Count;
            if (sourceCount == 0)
                return null;
            if (sourceCount <= targetCount)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ret[i] = list[i];
                }
                return ret;
            }
            indexList.Clear();
            for (int i = 0; i < sourceCount; i++)
                indexList.Add(i);
            int site = sourceCount;//设置下限
            int id;
            for (int j = 0; j < targetCount; j++)
            {
                //返回0到site - 1之中非负的一个随机数
                id = RandomHelper.RandomNumber(0, site);
                //在随机位置取出一个数，保存到结果数组
                ret[j] = list[indexList[id]];
                //最后一个数复制到当前位置
                indexList[id] = indexList[site - 1];
                //位置的下限减少一
                site--;
            }

            return ret;
        }

      
        public static bool GetParam(SkillParam skillParam, int skillId, out float value)
        {
            switch (skillParam.skillSourcetype)
            {
                case SkillSourcetype.DataTable:
                    SkillConfig skillConfig = ConfigHelper.Get<SkillConfig>(skillId);
                    if (skillConfig == null)
                    {
                        Log.Error($"skillConfig==null where skillId = {skillId}");
                        value = 0;
                        return false;
                    }
                    value = GetValueFromDataTable(skillParam.args, skillConfig);
                    return true;
                default:
                case SkillSourcetype.None:
                    value = 0;
                    return false;
                case SkillSourcetype.Input:
                    value = skillParam.value;
                    return true;
            }
            float GetValueFromDataTable(SkillDataTableArgs args, SkillConfig skillConfig)
            {
                switch (args)
                {
                    case SkillDataTableArgs.Args0:
                        return skillConfig.Args0;
                    case SkillDataTableArgs.Args1:
                        return skillConfig.Args1;
                    case SkillDataTableArgs.Args2:
                        return skillConfig.Args2;
                    case SkillDataTableArgs.Args3:
                        return skillConfig.Args3;
                    case SkillDataTableArgs.Args4:
                        return skillConfig.Args4;
                    case SkillDataTableArgs.Args5:
                        return skillConfig.Args5;
                    case SkillDataTableArgs.Args6:
                        return skillConfig.Args6;
                    case SkillDataTableArgs.Args7:
                        return skillConfig.Args7;
                    case SkillDataTableArgs.Args8:
                        return skillConfig.Args8;
                    case SkillDataTableArgs.Args9:
                        return skillConfig.Args9;
                    case SkillDataTableArgs.Args10:
                        return skillConfig.Args10;
                    case SkillDataTableArgs.Args11:
                        return skillConfig.Args11;
                    case SkillDataTableArgs.Args12:
                        return skillConfig.Args12;
                    case SkillDataTableArgs.Args13:
                        return skillConfig.Args13;
                    case SkillDataTableArgs.Args14:
                        return skillConfig.Args14;
                    case SkillDataTableArgs.Args15:
                        return skillConfig.Args15;
                    case SkillDataTableArgs.Args16:
                        return skillConfig.Args16;
                    case SkillDataTableArgs.Args17:
                        return skillConfig.Args17;
                    case SkillDataTableArgs.Args18:
                        return skillConfig.Args18;
                    case SkillDataTableArgs.Args19:
                        return skillConfig.Args19;
                    case SkillDataTableArgs.Args20:
                        return skillConfig.Args20;
                    case SkillDataTableArgs.Args21:
                    case SkillDataTableArgs.Args22:
                    case SkillDataTableArgs.Args23:
                    case SkillDataTableArgs.Args24:
                    case SkillDataTableArgs.Args25:
                    case SkillDataTableArgs.Args26:
                    case SkillDataTableArgs.Args27:
                    case SkillDataTableArgs.Args28:
                        throw new Exception($"类型错误 id = {skillConfig.Id},暂未实现");
                    case SkillDataTableArgs.Args29:
                        return skillConfig.Args29;
                    case SkillDataTableArgs.Args30:
                       return skillConfig.Args30;
                    default:
                        throw new Exception($"类型错误");
                }
                
            }
        }

        private static readonly List<int> indexList = new List<int>();
        private static readonly List<Unit> tempList = new List<Unit>();
        /// <summary>
        /// 从sourceList中选取count个随机目标
        /// </summary>
        public static void SelectUnits(List<Unit> sourceList, Unit selectUnit, int targetCount)
        {
            
            if (sourceList == null || sourceList.Count == 0) return;

            var sourceCount = sourceList.Count;

            if (sourceCount <= targetCount)
            {
                return;
            }
            tempList.Clear();
            indexList.Clear();
            
            tempList.AddRange(sourceList);
            sourceList.Clear();

            if (selectUnit != null)
            {
                targetCount--;
                sourceList.Add(selectUnit);
            }
          

            for (int i = 0; i < sourceCount; i++)
                indexList.Add(i);
            int site = sourceCount;//设置下限
            for (int j = 0; j < targetCount; j++)
            {
                //返回0到site - 1之中非负的一个随机数
                int id = RandomHelper.RandomNumber(0, site);
                //在随机位置取出一个数，保存到结果数组
                sourceList.Add(tempList[indexList[id]]);

                //最后一个数复制到当前位置
                indexList[id] = indexList[site - 1];
                //位置的下限减少一
                site--;
            }
        }
        public static int RandomNumber(int skillId, SkillParam minCount, SkillParam maxCount, bool isRnadom = true)
        {
            if (!SkillHelper.GetParam(minCount, skillId, out var min)
            || !SkillHelper.GetParam(maxCount, skillId, out var max))
            {
                Log.Error($"数据填写不完整，the id of skill is {skillId}");
                return 1;
            }

            return isRnadom ? RandomHelper.RandomNumber((int)min, (int)max) : (int)max;
        }
        public static int GetOverlableBuffMaxLayer(OverlableBuffType overlableBuffType)
        {
            switch (overlableBuffType)
            {
                default:
                case OverlableBuffType.无:
                    throw new Exception("类型错误：{overlableBuffType}");
                case OverlableBuffType.中毒:
                    return 8;
                case OverlableBuffType.流血:
                    return 6;
                case OverlableBuffType.燃烧:
                    return 4;
                case OverlableBuffType.治疗:
                    return 8;
            }
        }

        public static int GetThinkerInterval(ThinkerType thinkerType)
        {
            switch (thinkerType)
            {
                default:
                case ThinkerType.无:
                case ThinkerType.自定义:
                    throw new Exception("类型错误：{overlableBuffType}");
                case ThinkerType.中毒:
                    return 3500;
                case ThinkerType.流血:
                    return 5500;
                case ThinkerType.燃烧:
                    return 5000;
                case ThinkerType.治疗:
                    return 4000;
            }
        }

        public static int GetNomalSkillId(Unit unit)
        {
            //if(unit.UnitType == UnitType.Player)
            //{
            //    int jobId =  UserComponent.Instance.Get(unit.Id).JobId;
            //    switch (JobHelper.GetJobType(jobId))
            //    {
            //        default:
            //        case JobType.UnKnown:
            //            return 100001;
            //        case JobType.Officer:
            //            return 100001;
            //        case JobType.Sportsman:
            //            return 200001;
            //        case JobType.Nurse:
            //            return 300001;
            //        case JobType.Superman:
            //            return 400001;
            //    }
            //}
            return 100001;
        }
    }
}
