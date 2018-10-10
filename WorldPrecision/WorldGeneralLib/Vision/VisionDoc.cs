using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorldGeneralLib.Vision.Scenes;

namespace WorldGeneralLib.Vision
{
    public class VisionDoc
    {
        public int iCurrSceneIndex;
        public List<SceneData> listSceneData;

        public VisionDoc()
        {
            listSceneData = new List<SceneData>();
            iCurrSceneIndex = 0;
        }

        public static VisionDoc LoadDoc()
        {
            VisionDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisionDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/Vision/VisionDoc" + ".xml");
                pDoc = (VisionDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                //pDoc.dicSceneData = pDoc.listSceneData.ToDictionary(p => p.Name);

                foreach (SceneData item in pDoc.listSceneData)
                {
                    item.DataInit();
                }
            }
            catch
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new VisionDoc();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            try
            {
                if(!Directory.Exists(@".//Parameter/Vision/"))
                {
                    Directory.CreateDirectory(@".//Parameter/Vision/");
                }
                iCurrSceneIndex = VisionManage.iCurrSceneIndex;
                FileStream fs = new FileStream(@".//Parameter/Vision/VisionDoc.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisionDoc));
                xmlSerializer.Serialize(fs, this);
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }
    }
}
