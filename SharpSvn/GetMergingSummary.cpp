#include "Stdafx.h"

#include "Args/SvnMergingSummaryArgs.h"

using namespace SharpSvn;

bool SvnClient::GetMergingSummary(SvnTarget^ target, SvnTarget^ source, SvnMergingSummaryArgs^ args, [Out] SvnMergingSummaryEventArgs mergingSummary) {
    const char* yca_url;
    const char* base_url;
    const char* right_url;
    const char *target_url;
    svn_revnum_t yca_rev;
    svn_revnum_t base_rev;
    svn_revnum_t right_rev;
    svn_revnum_t target_rev;
    const char* repos_root_url;
    svn_boolean_t is_reintegration;

    svn_error_t *r = svn_client_get_merging_summary(
        &is_reintegration,
        &yca_url, &yca_rev,
        &base_url, &base_rev,
        &right_url, &right_rev,
        &target_url, &target_rev,
        &repos_root_url,
        source->AllocAsString(% _pool),
        source->GetSvnRevision(SvnRevision::Working, SvnRevision::Head)->AllocSvnRevision(% _pool),
        target->AllocAsString(% _pool),
        target->GetSvnRevision(SvnRevision::Working, SvnRevision::Head)->AllocSvnRevision(% _pool),
        CtxHandle,
        _pool.Handle,
        _pool.Handle);

    return args->HandleResult(this, r);
}
