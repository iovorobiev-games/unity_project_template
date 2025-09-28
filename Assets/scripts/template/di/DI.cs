using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace
{
    // DI class holds all the scopes the game uses.
    public class DI {
        public static Scope globalScope = new();

        public static Scope sceneScope = new();
    }

    // Scope holds all the instantiated variables and controls their lifecycle.
    // getInstance<>() will instantiate the object if it wasn't registered in scope and if it is possible to do
    // To start controlling already created instance by the scope call register(yourObject)
    // Note: for better clarity it is recommended to have only one scroll controlling instance.
    public class Scope
    {
        private Dictionary<Type, Object> stored = new();
        private Dictionary<Type, Dictionary<string, Object>> storedWithKey = new();
        private Dictionary<Type, HashSet<Object>> storedSet = new();

        private static Dictionary<Type, Dictionary<string, Type>> bindings = new();

        static Scope()
        {
            // Bind implementations to interfaces here
            // E.g. the below will bind PlayerBehaviour class to Behaviour interface
            // Whenever Behaviour is requested by scope.getInstance<Behaviour>(), PlayerBehaviour will be returned
            // bind<Behavior>(typeof(PlayerBehaviour));
            // In case different implementations should be bound to the same interface, use key bind instead
            // bind<Behavior>(typeof(PlayerBehaviour), "player1");    
            // bind<Behavior>(typeof(AiBehaviour), "player2");    
            // scope.getInstance<Behaviour>("player1") // returns PlayerBehaviour instance
        }
        public void register(Object o, string key = "")
        {
            Debug.Log("Registering " + o.GetType());
            var storedDict = storedWithKey.GetValueOrDefault(o.GetType(), new());
            storedDict[key] = o;
            storedWithKey[o.GetType()] = storedDict;
        }

        public void addToSet(Object o)
        {
            Debug.Log("Adding to set " + o.GetType());

            var set = storedSet.GetValueOrDefault(o.GetType(), new());
            set.Add(o);
            storedSet[o.GetType()] = set;
        }

        public T getInstance<T>(string key = "") where T : class
        {
            var binding = bindings.GetValueOrDefault(typeof(T), new());
            var actualType = typeof(T);
            if (binding.TryGetValue(key, out var value))
            {
                actualType = value;
            }

            var storedObject = storedWithKey.GetValueOrDefault(actualType, new());
            if (storedObject.ContainsKey(key))
            {
                return storedObject[key] as T;
            }

            var instantiated = (T) Activator.CreateInstance(actualType);
            register(instantiated, key);
            return instantiated;
        }

        public HashSet<T> getSetOf<T>()
        {
            var set = storedSet[typeof(T)];
            var result = new HashSet<T>();
            foreach (T item in set)
            {
                result.Add(item);
            }
            return result;
        } 

        public void clear()
        {
            stored.Clear();
        }

        public static void bind<T>(Type impl, string key = "")
        {
            var tBindings = bindings.GetValueOrDefault(typeof(T), new());
            tBindings[key] = impl;
            bindings[typeof(T)] = tBindings;
        }
    }
}