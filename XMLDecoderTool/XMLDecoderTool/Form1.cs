using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using ClosedXML.Excel;

namespace XMLDecoderTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = dialog.FileName;

                XDocument doc = XDocument.Load(dialog.FileName);

                var files = doc.Descendants("FILEHOSO").ToList();

                cboLoaiXML.Items.Clear();

                foreach (var file in files)
                {
                    string loai = file.Element("LOAIHOSO")?.Value;

                    if (!string.IsNullOrWhiteSpace(loai))
                    {
                        cboLoaiXML.Items.Add(loai);
                    }
                }

                if (cboLoaiXML.Items.Count > 0)
                {
                    cboLoaiXML.SelectedIndex = 0;
                }
            }
        }

        private void btnGiaiMa_Click(object sender, EventArgs e)
        {
            try
            {
                dgvData.DataSource = null;
                dgvData.Columns.Clear();
                string filePath = txtFilePath.Text;

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show("Vui lòng chọn file XML.");
                    return;
                }
                XDocument doc = XDocument.Load(filePath);
                var files = doc.Descendants("FILEHOSO").ToList();
                string loaiXML = cboLoaiXML.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(loaiXML))
                {
                    MessageBox.Show("Vui lòng chọn loại XML.");
                    return;
                }

                var file = files.FirstOrDefault(
                    x => x.Element("LOAIHOSO")?.Value == loaiXML
                );

                if (file == null)
                {
                    MessageBox.Show("Không tìm thấy XML1.");
                    return;
                }

                string base64 = file.Element("NOIDUNGFILE")?.Value ?? "";

                byte[] data = Convert.FromBase64String(base64);

                string xmlContent = Encoding.UTF8.GetString(data);

                XDocument innerDoc = XDocument.Parse(xmlContent);

                var records = LayDanhSachRecord(innerDoc, loaiXML);

                if (records.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy dữ liệu của {loaiXML}.");
                    return;
                }

                DataTable table = new DataTable();

                foreach (var record in records)
                {
                    foreach (var element in record.Elements())
                    {
                        string columnName = element.Name.LocalName;

                        if (!table.Columns.Contains(columnName))
                        {
                            table.Columns.Add(columnName);
                        }
                    }
                }

                foreach (var record in records)
                {
                    DataRow row = table.NewRow();

                    foreach (var element in record.Elements())
                    {
                        row[element.Name.LocalName] = element.Value;
                    }

                    table.Rows.Add(row);
                }

                dgvData.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Có lỗi xảy ra:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
        private List<XElement> LayDanhSachRecord(XDocument innerDoc, string loaiXML)
        {
            switch (loaiXML)
            {
                case "XML1":
                    return new List<XElement> { innerDoc.Root };

                case "XML2":
                    return innerDoc.Descendants("CHI_TIET_THUOC").ToList();

                case "XML3":
                    return innerDoc.Descendants("CHI_TIET_DVKT").ToList();

                case "XML4":
                    return innerDoc.Descendants("CHI_TIET_CLS").ToList();

                case "XML5":
                    return innerDoc.Descendants("CHI_TIET_DIEN_BIEN_BENH").ToList();

                default:
                    return new List<XElement>();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = txtFilePath.Text;

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    MessageBox.Show("Vui lòng chọn file XML.");
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();

                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.Title = "Lưu toàn bộ dữ liệu";
                saveDialog.FileName = "DuLieuXML.xlsx";

                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // Đọc file XML bên ngoài
                XDocument doc = XDocument.Load(filePath);

                var files = doc.Descendants("FILEHOSO").ToList();

                using (var workbook = new XLWorkbook())
                {
                    foreach (var file in files)
                    {
                        string loaiXML = file.Element("LOAIHOSO")?.Value ?? "";

                        string base64 = file.Element("NOIDUNGFILE")?.Value ?? "";

                        if (string.IsNullOrWhiteSpace(base64))
                        {
                            continue;
                        }

                        // Giải mã Base64
                        byte[] data = Convert.FromBase64String(base64);

                        // Chuyển sang UTF-8
                        string xmlContent = Encoding.UTF8.GetString(data);

                        // Đọc XML bên trong
                        XDocument innerDoc = XDocument.Parse(xmlContent);

                        // Lấy danh sách record
                        var records = LayDanhSachRecord(innerDoc, loaiXML);

                        if (records.Count == 0)
                        {
                            continue;
                        }

                        // Tạo DataTable
                        DataTable table = new DataTable();

                        foreach (var record in records)
                        {
                            foreach (var element in record.Elements())
                            {
                                string columnName = element.Name.LocalName;

                                if (!table.Columns.Contains(columnName))
                                {
                                    table.Columns.Add(columnName);
                                }
                            }
                        }

                        // Thêm dữ liệu
                        foreach (var record in records)
                        {
                            DataRow row = table.NewRow();

                            foreach (var element in record.Elements())
                            {
                                row[element.Name.LocalName] = element.Value;
                            }

                            table.Rows.Add(row);
                        }

                        // Tạo Sheet
                        var worksheet = workbook.Worksheets.Add(loaiXML);

                        // Ghi tiêu đề
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            worksheet.Cell(1, col + 1).Value =
                                table.Columns[col].ColumnName;
                        }

                        // Ghi dữ liệu
                        for (int row = 0; row < table.Rows.Count; row++)
                        {
                            for (int col = 0; col < table.Columns.Count; col++)
                            {
                                worksheet.Cell(row + 2, col + 1).Value =
                                    table.Rows[row][col]?.ToString();
                            }
                        }

                        // Định dạng tiêu đề
                        var header = worksheet.Range(
                            1,
                            1,
                            1,
                            table.Columns.Count
                        );

                        header.Style.Font.Bold = true;

                        header.Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Center;

                        header.Style.Alignment.Vertical =
                            XLAlignmentVerticalValues.Center;

                        // Bộ lọc
                        worksheet.Range(
                            1,
                            1,
                            table.Rows.Count + 1,
                            table.Columns.Count
                        ).SetAutoFilter();

                        // Cố định dòng tiêu đề
                        worksheet.SheetView.FreezeRows(1);

                        // Tự động điều chỉnh độ rộng
                        worksheet.Columns().AdjustToContents();
                    }

                    // Lưu toàn bộ workbook
                    workbook.SaveAs(saveDialog.FileName);
                }

                MessageBox.Show(
                    "Đã xuất toàn bộ dữ liệu XML ra Excel thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể xuất dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
