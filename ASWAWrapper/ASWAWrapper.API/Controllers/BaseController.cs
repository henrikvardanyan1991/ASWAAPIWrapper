using ASWAWrapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASWAWrapper.API.Controllers
{
    public abstract class BaseController : Controller
    {
        private List<ValidationMessageModel> _messages;
        protected List<ValidationMessageModel> GetVaidationMessages()
        {
            if (!ModelState.IsValid)
            {
                _messages = new List<ValidationMessageModel>();

                foreach (String key in ModelState.Keys)
                {
                    foreach (ModelError error in ModelState[key].Errors)
                    {
                        ValidationMessageModel validationMessage = new ValidationMessageModel
                        {
                            Code = error.ErrorMessage,
                            Key = key.StartsWith("model.") ? key.Replace("model.", String.Empty) : key,
                            Message = error.ErrorMessage,

                        };
                        _messages.Add(validationMessage);
                    }
                }
            }

            return _messages ?? new List<ValidationMessageModel>();

        }
    }
}
