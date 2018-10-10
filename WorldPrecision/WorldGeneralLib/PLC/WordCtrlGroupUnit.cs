using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class WordCtrlGroupUnit
    {
        public string m_strPlcName = "";
        public List<WordCtrlGroupItem> wordGroupList;
        public WordCtrlGroupUnit()
        {
            wordGroupList = new List<WordCtrlGroupItem>();
        }
        public bool addItemToGroup(DWordTextBox WordTextItem)
        {
            bool bAddOk = false;
            foreach (WordCtrlGroupItem wordGroupTemp in wordGroupList)
            {
                if (wordGroupTemp.CheckCanAddTo(WordTextItem) == 3)
                {
                   
                }
                else
                {
                    bAddOk = true;
                    break;
                }
            }
            if (!bAddOk)
            {
                WordCtrlGroupItem wordGroup = new WordCtrlGroupItem();
                wordGroupList.Add(wordGroup);
                wordGroup.CheckCanAddTo(WordTextItem);
            }
            return true;
        }
    }
}
