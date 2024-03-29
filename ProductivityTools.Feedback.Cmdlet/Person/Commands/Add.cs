﻿using PSTeamManagement.Cmdlet;
using Feedback.Cmdlet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Feedback.Cmdlet.Person
{
    public class Add : TeamManagmentCmdletBase<AddPerson>
    {
        public Add(AddPerson cmdlet) : base(cmdlet) { }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            ApiClient.AddPerson(Cmdlet.FirstName,Cmdlet.LastName,Cmdlet.Intials,Cmdlet.Category);
        }
    }
}
