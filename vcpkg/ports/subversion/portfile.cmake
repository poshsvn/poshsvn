vcpkg_download_distfile(ARCHIVE
    URLS "https://dlcdn.apache.org/subversion/subversion-1.15.0.zip"
    FILENAME "subversion-1.15.0.zip"
    SHA512 6fa25359d94d11662ffc77844a4f355dcc94634c4d5d1d8152829a494b7c4f950f15f0687812932b4bbf163ae97e718aadcacf74a218aa0907c71d05d652475e
)

vcpkg_extract_source_archive_ex(
    OUT_SOURCE_PATH SOURCE_PATH
    ARCHIVE "${ARCHIVE}"
)

vcpkg_cmake_configure(
    SOURCE_PATH "${SOURCE_PATH}"
    OPTIONS
        -DSVN_INSTALL_PRIVATE_H=ON
)

vcpkg_cmake_install()
