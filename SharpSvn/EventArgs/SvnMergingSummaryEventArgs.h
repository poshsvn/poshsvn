// Copyright 2007-2008 The SharpSvn Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

#pragma once

namespace SharpSvn {
    public ref class SvnMergingSummaryEventArgs : public SvnEventArgs
    {
        AprPool^ _pool;
        initonly SvnCommandType _commandType;

    internal:
        SvnMergingSummaryEventArgs(SvnCommandType commandType, AprPool^ pool)
        {
            if (!pool)
                throw gcnew ArgumentNullException("pool");

            _pool = pool;
            _commandType = commandType;
        }

    public:
        property SvnCommandType CurrentCommandType
        {
            SvnCommandType get()
            {
                return _commandType;
            }
        }

    protected public:
        virtual void Detach(bool keepProperties) override
        {
            try
            {
            }
            finally
            {
                _pool = nullptr;
            }
        }
    };
}
