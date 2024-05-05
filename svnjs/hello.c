#include <assert.h>
#include <node_api.h>
#include <svn_client.h>
#include <svn_pools.h>
#include <svn_dirent_uri.h>

static napi_value Method(napi_env env, napi_callback_info info) {
    apr_initialize();

    apr_pool_t* pool;
    apr_pool_create(&pool, NULL);

    svn_client_ctx_t *ctx;
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

    napi_status status;
    napi_value world;
    status = napi_create_string_utf8(env, "world", 5, &world);
    assert(status == napi_ok);
    return world;
}

#define DECLARE_NAPI_METHOD(name, func) { name, 0, func, 0, 0, 0, napi_default, 0 }

static napi_value Init(napi_env env, napi_value exports) {
    napi_status status;
    napi_property_descriptor desc = DECLARE_NAPI_METHOD("hello", Method);
    status = napi_define_properties(env, exports, 1, &desc);
    assert(status == napi_ok);
    return exports;
}

NAPI_MODULE(NODE_GYP_MODULE_NAME, Init)
