using System;
using System.Collections.Generic;

namespace Freitas.Octopus.Domain.Models
{
    public class Form : BaseModel
    {
        #region Fields

        private string title;
        private string description;
        private DateTimeOffset createdAt;
        private Guid createdBy;
        private IList<Question> questions;

        #endregion

        #region Properties

        public string Title { get => title; set => SetProperty(ref title, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public DateTimeOffset CreatedAt { get => createdAt; set => SetProperty(ref createdAt, value); }
        public Guid CreatedBy { get => createdBy; set => SetProperty(ref createdBy, value); }
        public IList<Question> Questions { get => questions; set => SetProperty(ref questions, value); }

        #endregion
    }
}
