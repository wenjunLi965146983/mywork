using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Table;

namespace WorldGeneralLib.Hardware
{
    public interface IMotionAction
    {
        double GetCurrentPos(short sAxis);
        bool GetHome(short sAxis);
        bool GetAlarm(short sAxis);
        bool GetLimtCW(short sAxis);
        bool GetLimtCCW(short sAxis);
        bool GetEstop(short sAxis);
        bool GetServoOn(short sAxis);
        bool IsMoving(short sAxis);
        bool IsMoveDone(short sAxis);
        bool SetVel(short sAxis, double dVel);
        double GetVel(short sAxis);
        bool JobMove(short sAxis, double dAcc, double dDec, double dVel);
        bool StopMove(short sAxis);
        bool HandleErrorMessage(short errorMessage);
        bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed);
        bool FinishSearchLimit(short axis);
        bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd);
        bool FinishSearchHome(short axis);
        bool SetAxisPos(short axis, double dPos);
        bool ZeroAxisPos(short axis);

        bool ServoOn(short axis);
        bool ServoOff(short axis);

        bool SetLimtOn(short axis);
        bool SetLimtOff(short axis);
        bool SetLimtDisable(short axis);
        bool SetHomeOn(short axis);
        bool SetHomeOff(short axis);
        bool SetNearHomeOn(short axis);
        bool SetNearHomeOff(short axis);
        bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos);
        bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos);

        bool SetAlarmOn(short axis);
        bool SetAlarmOff(short axis);
        bool SetPulseMode(short axis, PulseMode psm);

        bool LineXYZMove(short num, short sAxisX, short sAxisY, short sAxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ);
        bool ArcXYMove(short num, short sAxisX, short sAxisY, short sAxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW);
        void BuildCor(short num, short sAxisX, short sAxisY, short sAxisZ);
        bool ArcXYCZMove(short num, short sAxisX, short sAxisY, short sAxisZ, double posX, double posY, double posZ, double xCenter, double yCenter, short iCCW, double dSpeed, double dAcc, double velEnd, short fifo = 0);
        void InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed);
        void InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed);
        void StartCure(short num);
        bool CureMoveDone(short num, out int iStep);

        //For Robot
        bool RobotAbsPosMove(ushort index, double dPosX, double dPosY, double dPosZ, double dPosU, double dVel, bool bCheckPoint, ref int iErr);
    }
}
