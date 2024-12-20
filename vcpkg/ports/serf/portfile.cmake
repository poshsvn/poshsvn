vcpkg_download_distfile(ARCHIVE
    URLS "https://archive.apache.org/dist/serf/serf-1.3.10.zip"
    FILENAME "serf-1.3.10.zip"
    SHA512 82e1c7342b0fa102c0e853989da0f6b590584e5a1d7737f891edd1d49b2a3ec271fd71f2642813455f73230c57230aebdc3a83808335dd53c5ce9fdab8506e2f
)

vcpkg_extract_source_archive(
    SOURCE_PATH
    ARCHIVE "${ARCHIVE}"
)

file(COPY "${CMAKE_CURRENT_LIST_DIR}/CMakeLists.txt" DESTINATION "${SOURCE_PATH}")
file(COPY "${CMAKE_CURRENT_LIST_DIR}/build" DESTINATION "${SOURCE_PATH}")

vcpkg_cmake_configure(
    SOURCE_PATH "${SOURCE_PATH}"
    OPTIONS
        -DAPR_ROOT=${CURRENT_INSTALLED_DIR}
        -DSKIP_SHARED=ON
        -DSKIP_TESTS=ON
)

vcpkg_cmake_install()
vcpkg_copy_pdbs()

vcpkg_install_copyright(FILE_LIST "${SOURCE_PATH}/LICENSE")

configure_file("${SOURCE_PATH}/build/serf.pc.in" "${CURRENT_PACKAGES_DIR}/lib/pkgconfig/serf.pc" @ONLY)
