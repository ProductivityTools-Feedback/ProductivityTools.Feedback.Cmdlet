using PSTeamManagment.Cmdlet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.TeamManagement.Cmdlet.Person
{
 public class GetList : TeamManagmentCmdletBase<ListPeople>
    {
        public GetList(ListPeople cmdlet) : base(cmdlet) { }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            var result=ApiClient.GetPeopleList();
            foreach (var item in result.Result)
            {
                this.Cmdlet.WriteObject(item);
            }
        }
    }
}
