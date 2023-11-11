﻿using ProductivityTools.Feedback.Cmdlet.ClientCaller;
using PSTeamManagement.Cmdlet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTeamManagment.Cmdlet.Feedback.Commands
{
    public class Get : TeamManagmentCmdletBase<GetFeedback>
    {
        public Get(GetFeedback cmdlet) : base(cmdlet) { }

        protected override bool Condition => this.Cmdlet.Initials.AnyPersonInitial();

        protected override void Invoke()
        {
          
            var perfonFeedbackList = ApiClient.GetFeedback(this.Cmdlet.Initials.SplitToList());

            foreach (var person in perfonFeedbackList)
            {
                Console.WriteLine($"[{person.FirstName} {person.LastName}]");

                foreach (var item in person.Feedback)
                {
                    Console.WriteLine($"[{item.Date}]");
                    Console.WriteLine($"{item.Value}");
                    Console.WriteLine("");
                }
            }
        }
    }
}
