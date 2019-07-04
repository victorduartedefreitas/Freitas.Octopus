using Freitas.Octopus.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freitas.Octopus.Domain.Models
{
    public class Question : BaseModel
    {
        #region Fields

        private Guid formId;
        private QuestionTypes questionType;
        private string text;

        #endregion
    }
}
