using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cells
{
    public class Cell<T> : INotifyPropertyChanged
    {
        private T contents;

        public Cell(T initialContents = default(T))
        {
            this.contents = initialContents;
        }

        public virtual T Value
        {
            get
            {
                return contents;
            }
            set
            {
                this.contents = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        public Cell<U> Derive<U>(Func<T, U> transformer, Func<U, T> untransformer)
        {
            return new Derived<T, U>(this, transformer, untransformer);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Derived<IN, OUT> : Cell<OUT>
    {
        private readonly Cell<IN> dependency;

        private readonly Func<IN, OUT> transformer;

        private readonly Func<OUT, IN> untransformer;

        public Derived(Cell<IN> dependency, Func<IN, OUT> transformer, Func<OUT, IN> untransformer)
            : base(transformer(dependency.Value))
        {
            this.dependency = dependency;
            this.transformer = transformer;
            this.untransformer = untransformer;

            this.dependency.PropertyChanged += (sender, args) => base.Value = transformer(dependency.Value);
        }

        public override OUT Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                this.dependency.Value = untransformer(value);
            }
        }
    }
}
