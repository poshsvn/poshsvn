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

        initonly svn_boolean_t _is_reintegration;
        initonly const char* _yca_url;
        initonly svn_revnum_t _yca_rev;
        initonly const char* _base_url;
        initonly svn_revnum_t _base_rev;
        initonly const char* _right_url;
        initonly svn_revnum_t _right_rev;
        initonly const char* _target_url;
        initonly svn_revnum_t _target_rev;
        initonly const char* _repos_root_url;

    internal:
        SvnMergingSummaryEventArgs(
            svn_boolean_t is_reintegration,
            const char* yca_url, svn_revnum_t yca_rev,
            const char* base_url, svn_revnum_t base_rev,
            const char* right_url, svn_revnum_t right_rev,
            const char* target_url, svn_revnum_t target_rev,
            const char* repos_root_url,
            SvnCommandType commandType, AprPool^ pool)
        {
            if (!pool)
                throw gcnew ArgumentNullException("pool");

            _is_reintegration = is_reintegration;
            _yca_url = yca_url;
            _yca_rev = yca_rev;
            _base_url = base_url;
            _base_rev = base_rev;
            _right_url = right_url;
            _right_rev = right_rev;
            _target_url = target_url;
            _target_rev = target_rev;
            _repos_root_url = repos_root_url;

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

        property bool IsReintegration
        {
            bool get()
            {
                return _is_reintegration;
            }
        }

        property Uri^ YcaUrl
        {
            Uri^ get()
            {
                return SvnBase::Utf8_PtrToUri(_yca_url, SvnNodeKind::None);
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
