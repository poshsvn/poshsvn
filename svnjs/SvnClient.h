#pragma once

#include "pch.h"

class SvnClient : public Napi::ObjectWrap<SvnClient> {
public:
    static Napi::Object Init(Napi::Env env, Napi::Object exports);
    SvnClient(const Napi::CallbackInfo& info);

private:
   void Info(const Napi::CallbackInfo& info);
};
