﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace SchoolWeb.Infrastructure.Filters
{
    //Фильтр действий для запись в сессию данных из ModelState
    public class SetToSessionAttribute : Attribute, IActionFilter
    {

        private readonly string _name;//имя ключа
        public SetToSessionAttribute(string name)
        {
            _name = name;
        }

        // Выполняется до выполнения метода контроллера, но после привязки данных передаваемых в контроллер
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        // Выполняется после выполнения метода контроллера
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Dictionary<string, string> dict = [];
            // считывание данных из ModelState и запись в сессию
            if (context.ModelState.Count > 0)
            {
                foreach (var item in context.ModelState)
                {
                    dict.Add(item.Key, item.Value.AttemptedValue);
                }
                context.HttpContext.Session.Set(_name, dict);
            }

        }
    }
}