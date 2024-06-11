vcpkg_download_distfile(ARCHIVE
    URLS "https://dlcdn.apache.org/subversion/subversion-1.14.3.zip"
    FILENAME "subversion-1.14.3.zip"
    SHA512 36ff26f18bd90c28d04dd71603d563be511eb6acf35e1be383455acf822b17778aa217afe0a93e4b8eca4123d7002f43f49f8c26b1a448f98474bc171e81d45d
)

vcpkg_extract_source_archive_ex(
    OUT_SOURCE_PATH SOURCE_PATH
    ARCHIVE "${ARCHIVE}"
    PATCHES
        "svn-fix-ws2_32.patch"
        "svn-cmake.patch"
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

vcpkg_execute_build_process(
    COMMAND ${PYTHON3} gen-make.py -t cmake
    WORKING_DIRECTORY "${SOURCE_PATH}"
    LOGNAME "gen-make"
)

vcpkg_cmake_configure(
    SOURCE_PATH "${SOURCE_PATH}"
    OPTIONS
        -DSVN_SQLITE_AMALGAMATION_DIR=${SQLITE_AMALGAMATION_SOURCE_PATH}
)

vcpkg_cmake_install()

vcpkg_install_copyright(FILE_LIST "${SOURCE_PATH}/LICENSE")
