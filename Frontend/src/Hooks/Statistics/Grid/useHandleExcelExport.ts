import type { ExcelExport } from "@progress/kendo-react-excel-export";
import { useRef } from "react";

const useHandleExcelExport = () => {
  const _excelExport = useRef<ExcelExport | null>(null);

  const hanleExcelExport = () => {
    if (_excelExport.current !== null) {
      _excelExport.current.save();
    }
  };

  return { _excelExport, hanleExcelExport };
};

export default useHandleExcelExport;
