using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.Feedback.Cmdlet.Person
{
    [Cmdlet(VerbsCommon.Get, "PeopleList")]
    [Description("Add-PersonFeedback adds person to the feedback database")]
    public class ListPeople : PSCmdletPT
    {
        public ListPeople()
        {
            this.AddCommand(new GetList(this));
        }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }

}
