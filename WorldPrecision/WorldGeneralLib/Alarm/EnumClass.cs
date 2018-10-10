using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Alarm
{
    /// <summary>
    /// 报警输出或日志文件输出的消息等级
    /// </summary>
    public enum OutputLevel
    {
        Trace = 0,  //普通输出
        Debug,  //Debug输出
        Info,   //信息类型的消息
        Warn,   //警告信息
        Error,  //错误信息
        Fatal   //致命异常信息， 通常发生致命异常之后程序无法继续运行
    }
}
