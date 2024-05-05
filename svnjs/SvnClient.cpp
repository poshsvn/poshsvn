#include "pch.h"

#include "SvnClient.h"

Napi::Object Init(Napi::Env env, Napi::Object exports) {
    apr_initialize();

    exports.Set(Napi::String::New(env, "info"), Napi::Function::New(env, Info));

    return exports;
}

NODE_API_MODULE(addon, Init)
