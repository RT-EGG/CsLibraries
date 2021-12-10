@pushd %~dp0

@if exist output (
    rmdir /s /q output
)

doxygen Doxyfile

@popd
@exit /b 0
