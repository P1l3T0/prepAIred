import type { ExcelExport } from "@progress/kendo-react-excel-export";
import { useRef } from "react";

const useHandleExcelExport = () => {
  const _excelExport = useRef<ExcelExport | null>(null);

  const handleExcelExport = () => {
    if (_excelExport.current !== null) {
      _excelExport.current.save();
    }
  };

  return { _excelExport, handleExcelExport };
};

export default useHandleExcelExport;
