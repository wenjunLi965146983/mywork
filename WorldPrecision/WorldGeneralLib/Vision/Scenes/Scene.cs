using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Vision.Actions;
using WorldGeneralLib.Vision.Actions.AccurateSearch;
using WorldGeneralLib.Vision.Actions.BrightCorrect;
using WorldGeneralLib.Vision.Actions.Calculate;
using WorldGeneralLib.Vision.Actions.Circle;
using WorldGeneralLib.Vision.Actions.CircleSearch;
using WorldGeneralLib.Vision.Actions.EdgePosition;
using WorldGeneralLib.Vision.Actions.Gather;
using WorldGeneralLib.Vision.Actions.Line;
using WorldGeneralLib.Vision.Actions.Match;
using WorldGeneralLib.Vision.Actions.MultiSearch;
using WorldGeneralLib.Vision.Actions.PreProcess;
using WorldGeneralLib.Vision.Actions.SimpleBlob;
using WorldGeneralLib.Vision.Actions.Threshold;

namespace WorldGeneralLib.Vision.Scenes
{
    public enum SceneResponse : short
    {
        Error,
        Success
    }
    public class Scene
    {
        private SceneData _sceneData;
        public List<ActionBase> listAction;

        public delegate void EventSceneExecuted();
        public EventSceneExecuted eventSceneExecuted;

        public Scene(SceneData sceneData)
        {
            _sceneData = sceneData;
        }
        public void SceneInit()
        {
            try
            {
                if(null == _sceneData)
                {
                    return;
                }
                listAction = new List<ActionBase>();
                foreach (ActionDataBase item in _sceneData.listActionData)
                {
                    #region GroupReadImage
                    if (item.Group == ActionGroup.GroupReadImage)
                    {
                        // 图像采集

                        if (item.Type == ActionType.ActionGather)
                        {
                            ActionGather action = new ActionGather((ActionGatherData)item);
                            listAction.Add(action);
                        }
                    }
                    #endregion
                    #region GroupEnhance
                    if (item.Group == ActionGroup.GroupEnhance)
                    {
                        // 图像增强
                        if (item.Type == ActionType.ActionMatch)
                        {
                            ActionMatch action = new ActionMatch((ActionMatchData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionPreProcess)
                        {
                            ActionPreProcess action = new ActionPreProcess((ActionPreProcessData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionBrightCorrect)
                        {
                            ActionBrightCorrect action = new ActionBrightCorrect((ActionBrightCorrectData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionThreshold)
                        {
                            ActionThreshold action = new ActionThreshold((ActionThresholdData)item);
                            listAction.Add(action);
                        }


                    }
                    #endregion
                    #region GroupDetectionAndMeasurement
                    if (item.Group == ActionGroup.GroupDetectionAndMeasurement)
                    {
                        //检测和测量
                        if (item.Type == ActionType.ActionMultiSearch)
                        {
                            ActionMultiSearch action = new ActionMultiSearch((ActionMultiSearchData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionEdgePosition)
                        {
                            ActionEdgePosition action = new ActionEdgePosition((ActionEdgePositionData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionAccurateSearch)
                        {
                            ActionAccurateSearch action = new ActionAccurateSearch((ActionAccurateSearchData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionSimpleBlob)
                        {
                            ActionSimpleBlob action = new ActionSimpleBlob((ActionSimpleBlobData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionCircleSearch)
                        {
                            ActionCircleSearch action = new ActionCircleSearch((ActionCircleSearchData)item);
                            listAction.Add(action);
                        }

                    }
                    #endregion
                    #region GroupAssist
                    if (item.Group == ActionGroup.GroupAssist)
                    {
                        //辅助检查和测量
                        if (item.Type == ActionType.ActionLine)
                        {
                            ActionLine action = new ActionLine((ActionLineData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionCircle)
                        {
                            ActionCircle action = new ActionCircle((ActionCircleData)item);
                            listAction.Add(action);
                        }
                        if (item.Type == ActionType.ActionCalculate)
                        {
                            ActionCalculate action = new ActionCalculate((ActionCalculateData)item);
                            listAction.Add(action);
                        }

                    }
                    #endregion
                    #region GroupBranch
                    if (item.Group == ActionGroup.GroupBranch)
                    {
                        //分支处理


                    }
                    #endregion
                    #region GroupResultOutput
                    if(item.Group == ActionGroup.GroupResultOutput)
                    {
                        //结果输出

                    }
                    #endregion
                }
            }
            catch (Exception)
            {
            }
        }

        public SceneResponse SceneExecute()
        {
            try
            {
                if (null == listAction || listAction.Count < 1)
                {
                    return SceneResponse.Success;
                }
                else
                {
                    foreach(ActionBase action in listAction)
                    {
                        action.ActionExcute();
                    }
                }
                    
                
            }
            catch (Exception ex)
            {
                return SceneResponse.Error;
                MessageBox.Show(ex.Message);
                
            }
            return SceneResponse.Success;
        }
    }
}
        