@pushd %~dp0

@if exist ..\docs (
    rmdir /s /q ..\docs
)

doxygen Doxyfile

@popd
@exit /b 0
