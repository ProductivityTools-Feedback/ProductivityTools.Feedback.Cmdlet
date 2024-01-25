﻿using ProductivityTools.PSCmdlet;
using ProductivityTools.Feedback.Cmdlet.ClientCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Cmdlet
{
    public abstract class TeamManagmentCmdletBase<Type> : PSCommandPT<Type> where Type : PSCmdletPT
    {

        ApiClient apiClient;
        protected ApiClient ApiClient
        {
            get
            {
                if (apiClient==null)
                {
                    this.apiClient = new ApiClient();
                }
                return this.apiClient;
            }
        }


        public TeamManagmentCmdletBase(Type cmdletType) : base(cmdletType)
        {
        }
    }
}
