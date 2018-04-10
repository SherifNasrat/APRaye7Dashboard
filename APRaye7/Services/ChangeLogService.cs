using APRaye7.Models.ViewModels;
using CIBAdminsDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APRaye7.Services
{
    public class ChangeLogService : ServicesBase
    {
        public List<ChangeLogVM> getAllLogs()
        {
           // var listOfChangeLogs = CIBcontext.ChangeLogs.Select(log => log).OrderByDescending(log => log.LogDate).ToList();
            var listOfChangeLogs = (from l in CIBcontext.ChangeLogs
                        join sy in CIBcontext.SystemUsers
                        on l.FK_SystemUserID equals sy.SystemUserID
                        select new ChangeLogVM { ChangeLogId=l.ChangeLogId, UserName=sy.Username, Action=l.Action, Entity=l.Entity, RecordNumber=l.FK_RecordID, LogDate=l.LogDate.ToString() }).ToList();
            return listOfChangeLogs;

        }

        public List<ChangeLogDetailsVM> getAllDetailLogs(int? changeLogId)
        {
            var list = CIBcontext.ChangeLogDetails.Where(l => l.FK_ChangeLogID == changeLogId).Select(l =>new ChangeLogDetailsVM { ChangeLogDetailId=l.ChangeLogDetailsId ,Field=l.Field,PreviousValue=l.PreviousValue,NewValue=l.NewValue,DetailDate=l.DetailDate.ToString()}).ToList();
            return list;
        }
    }
}