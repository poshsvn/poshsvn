#include "pch.h"

#include "SvnClient.h"

typedef struct info_receiver_baton_t
{
    Napi::Function callback;
    Napi::Env env;
} info_receiver_baton_t;

static svn_error_t*
info_receiver(void* baton,
              const char* target,
              const svn_client_info2_t* info,
              apr_pool_t* pool)
{
    info_receiver_baton_t* const receiver_baton = (info_receiver_baton_t*)baton;
    Napi::Env env = receiver_baton->env;
    Napi::Function callback = receiver_baton->callback;

    callback.Call(env.Global(), { Napi::String::New(env, info->repos_root_URL) });

    return svn_error_create(1, NULL, NULL);
}

void SvnClient::Info(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    Napi::Function callback = info[0].As<Napi::Function>();

    info_receiver_baton_t receiver_baton = { callback, env };

    apr_pool_t* pool;
    apr_pool_create(&pool, NULL);

    svn_client_ctx_t* ctx;
    svn_client_create_context2(&ctx, NULL, pool);

    svn_client_info_receiver2_t receiver = info_receiver;

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
        info_receiver,
        &receiver_baton,
        ctx,
        pool);

    apr_pool_destroy(pool);
}
