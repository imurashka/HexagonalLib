using System;
using System.Text;

namespace HexagonalLib
{
    public class HexagonalException : Exception
    {
        private class Builder
        {
            private readonly StringBuilder _message = new StringBuilder();

            public Builder Append(string message)
            {
                _message.AppendLine(message);
                return this;
            }

            public Builder Append(HexagonalGrid grid)
            {
                Append(nameof(grid.Type), grid.Type);
                Append(nameof(grid.InscribedRadius), grid.InscribedRadius);
                Append(nameof(grid.OutscribedRadius), grid.OutscribedRadius);
                return this;
            }

            public Builder Append(params (string, object)[] fields)
            {
                foreach (var (paramName, paramValue) in fields)
                {
                    Append(paramName, paramValue);
                }

                return this;
            }

            private void Append(string paramName, object paramValue)
            {
                _message.Append($"{paramName}={paramValue}; ");
            }

            public override string ToString()
            {
                return _message.ToString();
            }
        }

        public HexagonalException(string message)
            : base(message)
        {
        }

        public HexagonalException(string message, params (string, object)[] fields)
            : base(CreateBuilder(message).Append(fields).ToString())
        {
        }

        public HexagonalException(string message, HexagonalGrid grid)
            : base(CreateBuilder(message).Append(grid).ToString())
        {
        }

        public HexagonalException(string message, HexagonalGrid grid, params (string, object)[] fields)
            : base(CreateBuilder(message).Append(grid).Append(fields).ToString())
        {
        }

        private static Builder CreateBuilder(string message)
        {
            return new Builder().Append(message);
        }
    }
}