﻿using Domain.Common;

namespace Domain.Entities.Product.ValueObjects
{
    public class ProjectTitle : ValueObject
    {
        public string Value { get; }
        public const int MinLength = 5;
        public const int MaxLength = 350;

        public ProjectTitle(string title)
        {
            //Guard(title);

            Value = title;
        }

        private void Guard(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException($"Project {nameof(title)} can not be null or empty.");
            }
            if (title.Length < MinLength || title.Length > MaxLength)
            {
                throw new ArgumentOutOfRangeException($"Project {nameof(title)} should be between {MinLength} and {MaxLength} characters length.");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Value))
            {
                AddError("", $"Project title can not be null or empty.", "");
            }

            if (Value.Length < MinLength || Value.Length > MaxLength)
            {
                AddError("", $"Project title should be between {MinLength} and {MaxLength} characters length.", "");
            }
        }
    }
}