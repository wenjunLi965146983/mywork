using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Vision.Scenes;

namespace WorldGeneralLib.Vision
{
    public class VisionManage
    {
        public static VisionDoc docVision;
        public static List<Scene> listScene;
        public const int MaxSceneCount = 20;
        public const int MaxCameraCount = 8;
        public static int iCurrSceneIndex;

        public static void LoadDoc()
        {
            docVision = VisionDoc.LoadDoc();
            iCurrSceneIndex = docVision.iCurrSceneIndex;
        }
        public static void VisionInit()
        {
            try
            {
                listScene = new List<Scene>();
                for(int index=docVision.listSceneData.Count; index<MaxSceneCount; index++)
                {
                    SceneData data = new SceneData(index.ToString());
                    docVision.listSceneData.Add(data);
                }
                foreach(SceneData item in docVision.listSceneData)
                {
                    Scene scene = new Scene(item);
                    listScene.Add(scene);
                }

                foreach(Scene scene in listScene)
                {
                    scene.SceneInit();
                }
            }
            catch (Exception ex)
            { 
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }
        }
    }
}
