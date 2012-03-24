﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Expressions
{
    public class BoundExpressionOptions
    {
        private bool _frozen;
        private bool _allowPrivateAccess;
        private bool _checked;
        private Type _resultType = typeof(object);

        public bool AllowPrivateAccess
        {
            get { return _allowPrivateAccess; }
            set
            {
                if (_frozen)
                    throw new InvalidOperationException("Options cannot be modified");

                _allowPrivateAccess = value;
            }
        }

        public Type ResultType
        {
            get { return _resultType; }
            set
            {
                if (_frozen)
                    throw new InvalidOperationException("Options cannot be modified");

                _resultType = value ?? typeof(object);
            }
        }

        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (_frozen)
                    throw new InvalidOperationException("Options cannot be modified");

                _checked = value;
            }
        }

        internal void Freeze()
        {
            _frozen = true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var other = obj as BoundExpressionOptions;

            return
                other != null &&
                _allowPrivateAccess == other._allowPrivateAccess &&
                _resultType == other._resultType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ObjectUtil.CombineHashCodes(
                    _allowPrivateAccess.GetHashCode(),
                    _resultType.GetHashCode()
                );
            }
        }

        internal BindingFlags AccessBindingFlags
        {
            get
            {
                if (_allowPrivateAccess)
                    return BindingFlags.Public | BindingFlags.NonPublic;
                else
                    return BindingFlags.Public;
            }
        }
    }
}