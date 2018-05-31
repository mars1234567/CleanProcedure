using Decontamination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanProcedure
{
    public class DataInfo
    {
          //获取分组和每个分组的步骤
          public static void GetStepInfo(ref  Dictionary<string, List<StepInfo>> infolist)
          {
              using (CleanProcedureEntities db = new CleanProcedureEntities())
              {
                  //刷卡器
                  var list = db.Clean_CardDevice.Select(p => p.CleanGroup).Distinct();
                  foreach (var item in list)
                  {
                      var machinlist = db.Clean_CardDevice.Where(p => p.CleanGroup == item && p.Stopped == false && p.DevType.Equals("消毒监控")).OrderBy(p => p.StepNumber);
                      if (machinlist.Count() == 0)
                          continue;
                      List<StepInfo> iList = new List<StepInfo>();
                      foreach (var i in machinlist)
                      {
                          StepInfo info = new StepInfo();
                          info.name = i.StepName;
                          iList.Add(info);
                      }
                      infolist.Add(item, iList);
                  }
              }
          }

          public static void GetCleanListInfo(ref  List<CleanListView> Cleanlist)
          {
              using (CleanProcedureEntities db = new CleanProcedureEntities())
              {
                  //刷卡器
                  Cleanlist = db.CleanListView.Select(o=>o).ToList();
 
              }
          }
    }
}
