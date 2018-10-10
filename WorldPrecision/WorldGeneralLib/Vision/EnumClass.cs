using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Vision
{
    public enum ActionResponse:short
    {
        OK,
        NG,
        NonExecution
    }
    public enum ActionType : short
    {
        /// <summary>
        /// 图像采集Action
        /// </summary>
        ActionGather,
            /// <summary>
            /// 位置修正Action
            /// </summary>
        ActionMatch,
         /// <summary>
         /// 图像预处理Action
         /// </summary>
        ActionPreProcess,
         /// <summary>
          /// 图像预处理Action
           /// </summary>
        ActionBrightCorrect,
        /// <summary>
        /// 图像搜索Action
        /// </summary>
        ActionMultiSearch,
        /// <summary>
        /// 灰度过滤Action
        /// </summary>
        ActionThreshold,
        /// <summary>
        /// 近似直线Action
        /// </summary>
        ActionLine,
        /// <summary>
        /// 近似直线Action
        /// </summary>
        ActionEdgePosition,
        /// <summary>
        /// 近似直线Action
        /// </summary>
        ActionCalculate,
        // <summary>
        /// 近似直线Action
        /// </summary>
        ActionAccurateSearch,
        ActionCircle,
        ActionSimpleBlob,
        ActionCircleSearch
    }
    public enum ActionGroup : short
    {
        /// <summary>
        /// 图像采集
        /// </summary>
        GroupReadImage,   
        /// <summary>
        /// 图像增强
        /// </summary>
        GroupEnhance,
        /// <summary>
        /// 检测和测量
        /// </summary>
        GroupDetectionAndMeasurement,  
        /// <summary>
        /// 辅助检查和测量
        /// </summary>
        GroupAssist,
        /// <summary>
        /// 分支处理
        /// </summary>
        GroupBranch,      
        /// <summary>
        /// 结果输出
        /// </summary>
        GroupResultOutput
    }
}
