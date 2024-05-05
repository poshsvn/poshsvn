#include <napi.h>

#include <svn_client.h>
#include <svn_pools.h>
#include <svn_dirent_uri.h>

Napi::String Info(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    apr_pool_t* pool;
    apr_pool_create(&pool, NULL);

    svn_client_ctx_t* ctx;
    svn_client_create_context2(&ctx, NULL, pool);

    svn_opt_revision_t rev;
    memset(&rev, 0, sizeof(rev));

    const char* path;

    svn_dirent_get_absolute(&path, "C:\\tima\\poshsvn-trunk", pool);
    path = svn_dirent_internal_style(path, pool);

    svn_error_t* r = svn_client_info4(
        path,
        &rev,
        &rev,
        svn_depth_empty,
        FALSE,
        FALSE,
        FALSE,
        NULL,
        NULL,
        NULL,
        ctx,
        pool);

    apr_pool_destroy(pool);

    return Napi::String::New(env, path);
}

Napi::Object Init(Napi::Env env, Napi::Object exports) {
    apr_initialize();

    exports.Set(Napi::String::New(env, "hello"), Napi::Function::New(env, Info));

    return exports;
}

NODE_API_MODULE(addon, Init)
