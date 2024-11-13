// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2024 Kybernetik //

using System;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Animancer.FSM
{
    /// https://kybernetik.com.au/animancer/api/Animancer.FSM/StateMachine_2
    partial class StateMachine<TKey, TState>
    {
        /// <summary>A <see cref="StateMachine{TKey, TState}"/> with a <see cref="DefaultKey"/>.</summary>
        /// <remarks>
        /// See <see cref="InitializeAfterDeserialize"/> if using this class in a serialized field.
        /// <para></para>
        /// <strong>Documentation:</strong>
        /// <see href="https://kybernetik.com.au/animancer/docs/manual/fsm/changing-states#default-states">
        /// Default States</see>
        /// </remarks>
        /// https://kybernetik.com.au/animancer/api/Animancer.FSM/WithDefault
        /// 
        [Serializable]
        public new class WithDefault : StateMachine<TKey, TState>
        {
            /************************************************************************************************************************/

            [SerializeField]
            private TKey _DefaultKey;

            /// <summary>The starting state and main state to return to when nothing else is active.</summary>
            /// <remarks>
            /// If the <see cref="CurrentState"/> is <c>null</c> when setting this value, it calls
            /// <see cref="ForceSetState(TKey)"/> to enter the specified state immediately.
            /// <para></para>
            /// For a character, this would typically be their <em>Idle</em> state.
            /// </remarks>
            public TKey DefaultKey
            {
                get => _DefaultKey;
                set
                {
                    _DefaultKey = value;
                    if (CurrentState == null && value != null)
                        ForceSetState(value);
                }
            }

            /************************************************************************************************************************/

            /// <summary>Calls <see cref="ForceSetState(TKey)"/> with the <see cref="DefaultKey"/>.</summary>
            /// <remarks>This delegate is cached to avoid allocating garbage when used in Animancer Events.</remarks>
            public readonly Action ForceSetDefaultState;

            /************************************************************************************************************************/

            /// <summary>Creates a new <see cref="StateMachine{TState}.WithDefault"/>.</summary>
            public WithDefault()
            {
                // Silly C# doesn't allow instance delegates to be assigned using field initializers.
                ForceSetDefaultState = () => ForceSetState(_DefaultKey);
            }

            /************************************************************************************************************************/

            /// <summary>Creates a new <see cref="StateMachine{TState}.WithDefault"/> and sets the <see cref="DefaultKey"/>.</summary>
            public WithDefault(TKey defaultKey)
                : this()
            {
                _DefaultKey = defaultKey;
                ForceSetState(defaultKey);
            }

            /************************************************************************************************************************/

            /// <inheritdoc/>
            public override void InitializeAfterDeserialize()
            {
                if (CurrentState != null)
                {
                    using (new KeyChange<TKey>(this, default, _DefaultKey))
                    using (new StateChange<TState>(this, null, CurrentState))
                        CurrentState.OnEnterState();
                }
                else
                {
                    ForceSetState(_DefaultKey);
                }

                // Don't call the base method.
            }

            /************************************************************************************************************************/

            /// <summary>Attempts to enter the <see cref="DefaultKey"/> and returns true if successful.</summary>
            /// <remarks>
            /// This method returns true immediately if the specified <see cref="DefaultKey"/> is already the
            /// <see cref="CurrentKey"/>. To allow directly re-entering the same state, use
            /// <see cref="TryResetDefaultState"/> instead.
            /// </remarks>
            public TState TrySetDefaultState() => TrySetState(_DefaultKey);

            /************************************************************************************************************************/

            /// <summary>Attempts to enter the <see cref="DefaultKey"/> and returns true if successful.</summary>
            /// <remarks>
            /// This method does not check if the <see cref="DefaultKey"/> is already the <see cref="CurrentKey"/>.
            /// To do so, use <see cref="TrySetDefaultState"/> instead.
            /// </remarks>
            public TState TryResetDefaultState() => TryResetState(_DefaultKey);

            /************************************************************************************************************************/
#if UNITY_EDITOR && UNITY_IMGUI
            /************************************************************************************************************************/

            /// <inheritdoc/>
            public override int GUILineCount => 2;

            /************************************************************************************************************************/

            /// <inheritdoc/>
            public override void DoGUI(ref Rect area)
            {
                area.height = UnityEditor.EditorGUIUtility.singleLineHeight;

                UnityEditor.EditorGUI.BeginChangeCheck();

                var state = StateMachineUtilities.DoGenericField(area, "Default Key", DefaultKey);

                if (UnityEditor.EditorGUI.EndChangeCheck())
                    DefaultKey = state;

                StateMachineUtilities.NextVerticalArea(ref area);

                base.DoGUI(ref area);
            }

            /************************************************************************************************************************/
#endif
            /************************************************************************************************************************/
            
#if UNITY_EDITOR && ODIN_INSPECTOR
            /// <summary>
            /// 由于<see cref="StateMachine{TKey, TState}"/>继承了<see cref="IDictionary{TKey, TValue}"/>接口，
            /// 如果项目安装了Odin插件，<see cref="StateMachine{TKey, TState}"/>在Inspector窗口上只会序列化成字典，且不会序列化其他的字段，
            /// 如 <see cref="_currentKey"/> 和 <see cref="WithDefault.DefaultKey"/>。
            /// <para></para>
            /// 定义此类后，Odin不会将<see cref="StateMachine{TKey, TState}"/>序列化成字典，且能序列化其他字段。
            /// <para></para>
            /// <see cref="ProcessedMemberPropertyResolver{T}"/>为一个抽象类，可以控制参数<see cref="T"/>在 Inspector 中如何序列化。
            /// <para></para>
            /// 使用[ResolverPriority(100)]修饰<see cref="SerializableResolver{T}"/>后，由于100大于默认的-5，
            /// <see cref="T"/>将以<see cref="SerializableResolver{T}"/>的形式被序列化，即非字典类型
            /// <remarks>
            /// 该类只是用来定义<see cref="T"/>的序列化规则，告诉Odin插件该如何序列化，无需实例化和使用
            /// </remarks></summary>
            [ResolverPriority(100)]
            private class SerializableResolver<T> : ProcessedMemberPropertyResolver<T> where T : StateMachine<TKey, TState> { }
#endif
        }
    }
}

