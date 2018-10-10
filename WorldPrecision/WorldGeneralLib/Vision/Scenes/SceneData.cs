using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
    [XmlInclude(typeof(ActionGatherData)),XmlInclude(typeof(ActionMatchData)), XmlInclude(typeof(ActionPreProcessData)), XmlInclude(typeof(ActionBrightCorrectData)), XmlInclude(typeof(ActionMultiSearchData)), XmlInclude(typeof(ActionThresholdData)), XmlInclude(typeof(ActionLineData)), XmlInclude(typeof(ActionEdgePositionData)), XmlInclude(typeof(ActionCalculateData)), XmlInclude(typeof(ActionAccurateSearchData)), XmlInclude(typeof(ActionCircleData)),XmlInclude(typeof(ActionSimpleBlobData)), XmlInclude(typeof(ActionCircleSearchData))]
    public class SceneData
    {
        public List<ActionDataBase> listActionData;

        public SceneData()
        {
            listActionData = new List<ActionDataBase>();
        }
        public SceneData(string strName):this()
        {
            _strName = strName;
        }

        private string _strName;
        public string Name
        {
            get { return _strName; }
            set {_strName = value; }
        }

        public void DataInit()
        {

        }
    }
}
