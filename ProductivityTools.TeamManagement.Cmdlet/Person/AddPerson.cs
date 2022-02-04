using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Management.Automation;
using System.Text;

namespace ProductivityTools.TeamManagement.Cmdlet.Person
{
    [Cmdlet(VerbsCommon.Add, "PersonFeedback")]
    [Description("Add-PersonFeedback adds person to the feedback database")]
    public class AddPerson : PSCmdletPT
    {
        public AddPerson()
        {
            this.AddCommand(new Add(this));
        }

        [Parameter(Position = 0)]
        [Description("First name")]
        public string FirstName { get; set; }

        [Parameter(Position = 0)]
        [Description("Last name")]
        public string LastName{ get; set; }

        [Parameter(Position = 0)]
        [Description("Initials")]
        public string Intials { get; set; }

        [Parameter(Position = 0)]
        [Description("Category")]
        public string Category{ get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessCommands();
            base.ProcessRecord();
        }
    }
}
