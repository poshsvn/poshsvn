#include "pch.h"

#include "SvnClient.h"

Napi::Object SvnClient::Init(Napi::Env env, Napi::Object exports) {
    Napi::Function func = DefineClass(
        env,
        "SvnClient",
        {
            InstanceMethod("info", &SvnClient::Info)
        });

    Napi::FunctionReference* constructor = new Napi::FunctionReference();
    *constructor = Napi::Persistent(func);
    env.SetInstanceData(constructor);

    exports.Set("SvnClient", func);
    return exports;
}

SvnClient::SvnClient(const Napi::CallbackInfo& info) : Napi::ObjectWrap<SvnClient>(info)
{
}

Napi::Object Init(Napi::Env env, Napi::Object exports) {
    apr_initialize();

    return SvnClient::Init(env, exports);
}

NODE_API_MODULE(addon, Init)
