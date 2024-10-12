vcpkg_download_distfile(ARCHIVE
    URLS "https://dlcdn.apache.org/subversion/subversion-1.14.4.zip"
    FILENAME "subversion-1.14.4.zip"
    SHA512 663e39543205d115c04142a24e9ae86a334142f5d8ba0dca58985df95eb21fe84036034ad6b1483f46e9d244f099b8a171b1a7337bc9c00f193838fe7f46ab89
)

vcpkg_extract_source_archive_ex(
    OUT_SOURCE_PATH SOURCE_PATH
    ARCHIVE "${ARCHIVE}"
    PATCHES
        "fix-serfdir.patch"
        "fix-expatdir.patch"
)

vcpkg_check_features(OUT_FEATURE_OPTIONS FEATURE_OPTIONS
    FEATURES
        ra-serf RA_SERF
)

vcpkg_download_distfile(SQLITE_AMALGAMATION_ARCHIVE
    URLS https://www.sqlite.org/2024/sqlite-amalgamation-3460000.zip
    FILENAME sqlite-amalgamation-3460000.zip
    SHA512 b38befaec5b3c32a35536f22f8e1dbb7a1859a6b354ad0fbdfb28634f2fab5acaa4d418420d52c4ab5291784203d46af16c183f113c4d2b4ce7efaa3a2a31d30
)

vcpkg_extract_source_archive_ex(
    OUT_SOURCE_PATH SQLITE_AMALGAMATION_SOURCE_PATH
    ARCHIVE "${SQLITE_AMALGAMATION_ARCHIVE}"
)

vcpkg_find_acquire_program(PYTHON3)

if(VCPKG_LIBRARY_LINKAGE STREQUAL static)
  set(BUILD_MODE --with-static-apr --with-static-openssl --disable-shared)
endif()

if(${RA_SERF})
    set(RA_SERF_OPTIONS --with-serf=${CURRENT_INSTALLED_DIR})
endif()

vcpkg_execute_build_process(
    COMMAND ${PYTHON3} gen-make.py
        -t vcproj --vsnet-version=2022
        --with-apr=${CURRENT_INSTALLED_DIR}
        --with-apr-util=${CURRENT_INSTALLED_DIR}
        --with-openssl=${CURRENT_INSTALLED_DIR}
        --with-serf=${CURRENT_INSTALLED_DIR}
        --with-sqlite=${SQLITE_AMALGAMATION_SOURCE_PATH}
        --with-zlib=${CURRENT_INSTALLED_DIR}
        ${RA_SERF_OPTIONS}
        ${BUILD_MODE}
    WORKING_DIRECTORY "${SOURCE_PATH}"
    LOGNAME "gen-make"
)

vcpkg_install_msbuild(
    SOURCE_PATH "${SOURCE_PATH}"
    PROJECT_SUBPATH "subversion_vcnet.sln"
    TARGET "__ALL__"
)

vcpkg_install_copyright(FILE_LIST "${SOURCE_PATH}/LICENSE")

file(COPY "${SOURCE_PATH}/subversion/include" DESTINATION "${CURRENT_PACKAGES_DIR}")
