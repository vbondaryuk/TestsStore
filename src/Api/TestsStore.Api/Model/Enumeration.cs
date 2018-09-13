﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestsStore.Api.Model
{
	public abstract class Enumeration : IComparable
	{
		public string Name { get; private set; }
		public Guid Id { get; private set; }

		protected Enumeration() { }

		protected Enumeration(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public override string ToString() => Name;

		public static IEnumerable<T> GetAll<T>() where T : Enumeration
		{
			var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

			return fields.Select(f => f.GetValue(null)).Cast<T>();
		}

		public override bool Equals(object obj)
		{
			var otherValue = obj as Enumeration;

			if (otherValue == null)
			{
				return false;
			}

			var typeMatches = GetType() == obj.GetType();
			var valueMatches = Id.Equals(otherValue.Id);

			return typeMatches && valueMatches;
		}

		public int CompareTo(object other)
		{
			return Id.CompareTo(((Enumeration)other).Id);
		}

		public override int GetHashCode() => Id.GetHashCode();
	}
}