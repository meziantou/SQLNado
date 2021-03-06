﻿using System;
using System.Linq.Expressions;

namespace SqlNado
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class SQLiteColumnAttribute : Attribute, IComparable, IComparable<SQLiteColumnAttribute>
    {
        // because Guid.Empty is not a const
        public const string GuidEmpty = "00000000-0000-0000-0000-000000000000";

        internal bool? _ignore;
        internal bool? _isNullable;
        internal bool? _isPrimaryKey;
        internal bool? _isReadOnly;
        internal bool? _hasDefaultValue;
        internal bool? _isDefaultValueIntrinsic;
        internal bool? _autoIncrements;
        internal int? _sortOrder;

        public virtual string Name { get; set; }
        public virtual string DataType { get; set; }
        public virtual Type ClrType { get; set; }
        public virtual string Collation { get; set; }
        public virtual bool Ignore { get => _ignore ?? false; set => _ignore = value; }
        public virtual SQLiteAutomaticColumnType AutomaticType { get; set; }
        public virtual bool AutoIncrements { get => _autoIncrements ?? false; set => _autoIncrements = value; }
        public virtual bool IsPrimaryKey { get => _isPrimaryKey ?? false; set => _isPrimaryKey = value; }
        public virtual SQLiteDirection PrimaryKeyDirection { get; set; }
        public virtual bool IsUnique { get; set; }
        public virtual string CheckExpression { get; set; }
        public virtual bool IsNullable { get => _isNullable ?? false; set => _isNullable = value; }
        public virtual bool IsReadOnly { get => _isReadOnly ?? false; set => _isReadOnly = value; }
        public virtual bool InsertOnly { get; set; }
        public virtual bool UpdateOnly { get; set; }
        public virtual bool HasDefaultValue { get => _hasDefaultValue ?? false; set => _hasDefaultValue = value; }
        public virtual bool IsDefaultValueIntrinsic { get => _isDefaultValueIntrinsic ?? false; set => _isDefaultValueIntrinsic = value; }
        public virtual int SortOrder { get => _sortOrder ?? -1; set => _sortOrder = value; }
        public virtual SQLiteBindOptions BindOptions { get; set; }
        public virtual object DefaultValue { get; set; }
        
        public virtual Expression<Func<object, object>> GetValueExpression { get; set; }
        public virtual Expression<Action<SQLiteLoadOptions, object, object>> SetValueExpression { get; set; }

        public override string ToString() => Name;
        int IComparable.CompareTo(object obj) => CompareTo(obj as SQLiteColumnAttribute);

        public virtual int CompareTo(SQLiteColumnAttribute other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (!_sortOrder.HasValue)
            {
                if (other._sortOrder.HasValue)
                    return 1;

                return 0;
            }

            if (!other._sortOrder.HasValue)
                return -1;

            return _sortOrder.Value.CompareTo(other._sortOrder.Value);
        }
    }
}
