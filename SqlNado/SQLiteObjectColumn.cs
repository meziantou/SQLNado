﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SqlNado
{
    public class SQLiteObjectColumn
    {
        public SQLiteObjectColumn(SQLiteObjectTable table, string name, Func<SQLiteObjectColumn, object, object> getValueFunc)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (getValueFunc == null)
                throw new ArgumentNullException(nameof(getValueFunc));

            Table = table;
            Name = name;
            GetValueFunc = getValueFunc;
        }

        public SQLiteObjectTable Table { get; }
        public string Name { get; }
        public Func<SQLiteObjectColumn, object, object> GetValueFunc { get; }
        public virtual bool IsNullable  { get; set; }
        public virtual bool IsReadOnly { get; set; }
        public virtual bool IsPrimaryKey { get; set; }

        public virtual object GetValue(object obj) => GetValueFunc(this, obj);

        public override string ToString()
        {
            string s = Name;
            if (IsPrimaryKey)
            {
                s += " (PK)";
            }

            if (IsNullable)
            {
                s += " (N)";
            }

            if (IsReadOnly)
            {
                s += " (RO)";
            }
            return s;
        }

        public virtual void CopyAttributes(SQLiteColumnAttribute attribute)
        {
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            IsReadOnly = attribute.IsReadOnly;
            IsNullable = attribute.IsNullable;
            IsPrimaryKey = attribute.IsPrimaryKey;
        }
    }
}