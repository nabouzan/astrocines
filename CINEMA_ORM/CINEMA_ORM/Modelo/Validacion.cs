﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CINEMA_ORM.Modelo
{
    public class Validacion
    {
        public static string errores(Object obj)
        {
            string mensError = "";

            ValidationContext validationContext = new ValidationContext(obj, null, null);
            List<ValidationResult> errores = new List<ValidationResult>();
            Validator.TryValidateObject(obj, validationContext, errores, true);

            if (errores.Count() > 0)
            {
                string mensageErrores = string.Empty;
                foreach (var error in errores)
                {
                    error.MemberNames.First();

                    mensError += error.ErrorMessage + "\n";
                    // mensError += error.ErrorMessage + Environment.NewLine;
                }
                return mensError;
            }
            else
            {
                return mensError;
            }
        }
    }
}
