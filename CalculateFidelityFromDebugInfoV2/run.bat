set ideal=%1
set columnIndex_idealset_query=%2
set columnIndex_idealset_doc=%3
set columnIndex_idealset_rating=%4
shift
shift
shift
shift

copy %1 gzin.gz
del %1 /F /Q

copy gzin.gz in  
gunzip -f gzin.gz 
copy /Y gzin in  

del gzin /F /Q
del gzin.gz /F /Q

CalculateFidelityFromDebugInfoV2.exe %ideal% %columnIndex_idealset_query% %columnIndex_idealset_doc% %columnIndex_idealset_rating% in %2 %3 %4 %5 %6 %7 %8
