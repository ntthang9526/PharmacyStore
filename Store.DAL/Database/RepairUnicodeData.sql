-- Khôi phục chữ tiếng Việt trong dữ liệu mẫu từ InitDatabase.sql ngày 08/11/2026.
-- Sao lưu trước khi chạy. Chọn PharmacyStoreDB trong SSMS rồi nhấn F5.
-- Chỉ sửa ô có đúng ID và đúng dạng mất dấu Windows-1252 của giá trị mẫu.
-- Không đoán dữ liệu tự nhập; không sửa tài khoản, số tiền, tồn kho hoặc thêm/xóa bản ghi.
-- File UTF-8 có BOM; sqlcmd cần -f 65001.
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON; SET CONCAT_NULL_YIELDS_NULL ON; SET ARITHABORT ON; SET NUMERIC_ROUNDABORT OFF;
IF UNICODE(N'ế')<>7871 OR UNICODE(N'đ')<>273 OR UNICODE(N'ộ')<>7897
 THROW 50002,'File SQL khong dung Unicode. Mo lai file bang UTF-8; voi sqlcmd dung -f 65001.',1;
IF DB_NAME()<>N'PharmacyStoreDB' AND DB_NAME() NOT LIKE N'PharmacyWebIntegration[_]%'
 THROW 50001,N'Chọn đúng database PharmacyStoreDB trước khi chạy.',1;
DECLARE @Fixed int=0;
BEGIN TRY
BEGIN TRAN;
-- Categories.Name
UPDATE t SET [Name]=s.Original
FROM dbo.[Categories] t JOIN (VALUES
(1, CAST(N'Thuốc giảm đau - hạ sốt' AS nvarchar(max)), N'Thu?c gi?m ?au - h? s?t'),
(3, N'Vitamin & Khoáng chất', N'Vitamin & Khoáng ch?t'),
(234, N'Thuốc thông dụng', N'Thu?c thông d?ng'),
(235, N'Vật tư y tế', N'V?t t? y t?'),
(236, N'Thuốc kê đơn', N'Thu?c kê ??n'),
(237, N'Thuốc khác', N'Thu?c khác'),
(266, N'Thuốc da liễu', N'Thu?c da li?u'),
(267, N'Thuốc dị ứng', N'Thu?c d? ?ng'),
(268, N'Thuốc hô hấp', N'Thu?c hô h?p'),
(269, N'Thuốc Mắt, Tai, Mũi, Họng', N'Thu?c M?t, Tai, M?i, H?ng'),
(270, N'Thuốc tiêu hoá & gan mật', N'Thu?c tiêu hoá & gan m?t')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Name] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Name])=DATALENGTH(s.Original)
 AND (t.[Name] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Name] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Categories.Description
UPDATE t SET [Description]=s.Original
FROM dbo.[Categories] t JOIN (VALUES
(266, CAST(N'Phân nhóm tham khảo danh mục thuốc Long Châu ngày 07/11/2026.' AS nvarchar(max)), N'Phân nhóm tham kh?o danh m?c thu?c Long Châu ngày 07/11/2026.'),
(267, N'Phân nhóm tham khảo danh mục thuốc Long Châu ngày 07/11/2026.', N'Phân nhóm tham kh?o danh m?c thu?c Long Châu ngày 07/11/2026.'),
(268, N'Phân nhóm tham khảo danh mục thuốc Long Châu ngày 07/11/2026.', N'Phân nhóm tham kh?o danh m?c thu?c Long Châu ngày 07/11/2026.'),
(269, N'Phân nhóm tham khảo danh mục thuốc Long Châu ngày 07/11/2026.', N'Phân nhóm tham kh?o danh m?c thu?c Long Châu ngày 07/11/2026.'),
(270, N'Phân nhóm tham khảo danh mục thuốc Long Châu ngày 07/11/2026.', N'Phân nhóm tham kh?o danh m?c thu?c Long Châu ngày 07/11/2026.')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Description] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Description])=DATALENGTH(s.Original)
 AND (t.[Description] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Description] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Medicines.Name
UPDATE t SET [Name]=s.Original
FROM dbo.[Medicines] t JOIN (VALUES
(243, CAST(N'Panadol Cảm Cúm' AS nvarchar(max)), N'Panadol C?m Cúm'),
(297, N'Nước muối 0,9%', N'N??c mu?i 0,9%'),
(298, N'Băng gạc', N'B?ng g?c'),
(299, N'Bông y tế', N'Bông y t?'),
(300, N'Gel rửa tay', N'Gel r?a tay'),
(301, N'Khẩu trang', N'Kh?u trang'),
(413, N'Thuốc Clorpheniramin 4 DHG (Hộp 10 Vỉ x 20 Viên)', N'Thu?c Clorpheniramin 4 DHG (H?p 10 V? x 20 Viên)'),
(414, N'Thuốc Allerphast 180mg Mebiphar (Hộp 1 Vỉ x 10 Viên)', N'Thu?c Allerphast 180mg Mebiphar (H?p 1 V? x 10 Viên)'),
(415, N'Thuốc Histalong - L 5mg Dr. Reddy''s (Hộp 2 vỉ x 10 viên)', N'Thu?c Histalong - L 5mg Dr. Reddy''s (H?p 2 v? x 10 viên)'),
(416, N'Thuốc Exopadin 60mg Trường Thọ (Hộp 3 Vỉ x 10 Viên)', N'Thu?c Exopadin 60mg Tr??ng Th? (H?p 3 V? x 10 Viên)'),
(417, N'Thuốc Cetirizin 10mg Trường Thọ (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Cetirizin 10mg Tr??ng Th? (H?p 10 V? x 10 Viên)'),
(418, N'Thuốc Clorpheniramin 4mg Khapharco (Hộp 10 Vỉ x 20 Viên)', N'Thu?c Clorpheniramin 4mg Khapharco (H?p 10 V? x 20 Viên)'),
(419, N'Thuốc Cetirizine Stada 10mg (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Cetirizine Stada 10mg (H?p 10 V? x 10 Viên)'),
(420, N'Viên nén phân tán trong miệng Loratadin 10mg An Thiên (Hộp 10 Vỉ x 10 Viên)', N'Viên nén phân tán trong mi?ng Loratadin 10mg An Thiên (H?p 10 V? x 10 Viên)'),
(421, N'Thuốc Fexofenadine Hydrochloride 180mg An Thiên (Hộp 3 Vỉ x 10 Viên)', N'Thu?c Fexofenadine Hydrochloride 180mg An Thiên (H?p 3 V? x 10 Viên)'),
(422, N'Thuốc Vomina Plus 50mg Medipharco (Hộp 25 Vỉ x 4 Viên)', N'Thu?c Vomina Plus 50mg Medipharco (H?p 25 V? x 4 Viên)'),
(423, N'Thuốc Fefasdin 60 Khapharco (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Fefasdin 60 Khapharco (H?p 10 V? x 10 Viên)'),
(424, N'Viên sủi Efferalgan 500mg UPSA SAS (Hộp 4 Vỉ x 4 Viên)', N'Viên s?i Efferalgan 500mg UPSA SAS (H?p 4 V? x 4 Viên)'),
(425, N'Thuốc Eugica MEGA We care (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Eugica MEGA We care (H?p 10 V? x 10 Viên)'),
(426, N'Siro HoAstex-S OPC (Hộp 30 Gói x 5ml)', N'Siro HoAstex-S OPC (H?p 30 Gói x 5ml)'),
(428, N'Siro Deslotid OPV (Hộp x 60ml)', N'Siro Deslotid OPV (H?p x 60ml)'),
(429, N'Siro Synatura Syrup AhnGook (Hộp 9 Gói x 10ml)', N'Siro Synatura Syrup AhnGook (H?p 9 Gói x 10ml)'),
(431, N'Thuốc ho người lớn OPC (Chai x 90ml)', N'Thu?c ho ng??i l?n OPC (Chai x 90ml)'),
(432, N'Viên ngậm Strepsils Throat Irritation & Cough Reckitt Benckiser (Hộp 2 Vỉ x 12 Viên)', N'Viên ng?m Strepsils Throat Irritation & Cough Reckitt Benckiser (H?p 2 V? x 12 Viên)'),
(433, N'Siro Ambroxol Danapha (Hộp X 60ml)', N'Siro Ambroxol Danapha (H?p X 60ml)'),
(434, N'Dung dịch xịt mũi Otrera 0.1% An Thiên (Hộp x 10ml)', N'Dung d?ch x?t m?i Otrera 0.1% An Thiên (H?p x 10ml)'),
(435, N'Thuốc nhỏ mắt V.Rohto Vitamin (Chai x 13ml)', N'Thu?c nh? m?t V.Rohto Vitamin (Chai x 13ml)'),
(436, N'Thuốc nhỏ mắt Sancoba (Chai X 5ml)', N'Thu?c nh? m?t Sancoba (Chai X 5ml)'),
(437, N'Ống hít Sao Vàng Danapha (Ống x 1.5g)', N'?ng hít Sao Vàng Danapha (?ng x 1.5g)'),
(438, N'Thuốc nhỏ mắt, mũi Natri Clorid 0,9% Traphaco (Lốc 10 Chai x 10ml)', N'Thu?c nh? m?t, m?i Natri Clorid 0,9% Traphaco (L?c 10 Chai x 10ml)'),
(439, N'Thuốc nhỏ mắt, nhỏ mũi Natri clorid 0.9% Bidiphar (Hộp 1 Lọ X 10ml)', N'Thu?c nh? m?t, nh? m?i Natri clorid 0.9% Bidiphar (H?p 1 L? X 10ml)'),
(440, N'Hỗn dịch uống Simegaz Plus OPV vị bạc hà (Hộp 20 Gói X 10ml)', N'H?n d?ch u?ng Simegaz Plus OPV v? b?c hà (H?p 20 Gói X 10ml)'),
(441, N'Thuốc Đại Tràng Trường Phúc (Hộp 3 Vỉ x 10 Viên)', N'Thu?c ??i Tràng Tr??ng Phúc (H?p 3 V? x 10 Viên)'),
(442, N'Thuốc Bổ Gan Trường Phúc (Hộp 3 Vỉ x 10 Viên)', N'Thu?c B? Gan Tr??ng Phúc (H?p 3 V? x 10 Viên)'),
(443, N'Hỗn dịch Gaviscon Dual Action Reckitt Benckiser (Hộp 24 Gói X 10ml)', N'H?n d?ch Gaviscon Dual Action Reckitt Benckiser (H?p 24 Gói X 10ml)'),
(444, N'Viên mật nghệ Cholapan OPC (Hộp 5 Vỉ x 10 Viên)', N'Viên m?t ngh? Cholapan OPC (H?p 5 V? x 10 Viên)'),
(445, N'Hỗn dịch uống Fudophos Phương Đông (Hộp 30 Gói x 5g)', N'H?n d?ch u?ng Fudophos Ph??ng ?ông (H?p 30 Gói x 5g)'),
(446, N'Thuốc Loperamide 2mg Stella Pharm (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Loperamide 2mg Stella Pharm (H?p 10 V? x 10 Viên)'),
(447, N'Thuốc Duphalac Abbott (Hộp 20 Gói x 15ml)', N'Thu?c Duphalac Abbott (H?p 20 Gói x 15ml)'),
(448, N'Thuốc bột pha hỗn dịch uống Smecta Ipsen hương cam & vani (Hộp 10 Gói)', N'Thu?c b?t pha h?n d?ch u?ng Smecta Ipsen h??ng cam & vani (H?p 10 Gói)'),
(449, N'Thuốc bột pha hỗn dịch uống Smecta Ipsen hương cam & vani (Hộp 30 Gói x 3g)', N'Thu?c b?t pha h?n d?ch u?ng Smecta Ipsen h??ng cam & vani (H?p 30 Gói x 3g)'),
(450, N'Viên nhai Kremil-S United (Hộp 10 Vỉ x 10 Viên)', N'Viên nhai Kremil-S United (H?p 10 V? x 10 Viên)'),
(451, N'Viên sủi Berocca Bayer (Tuýp 10 Viên)', N'Viên s?i Berocca Bayer (Tuýp 10 Viên)'),
(452, N'Thuốc Farzincol Pharmedic (Hộp 10 vỉ x 10 viên)', N'Thu?c Farzincol Pharmedic (H?p 10 v? x 10 viên)'),
(453, N'Siro Ceelin + Z United (Hộp x 60ml)', N'Siro Ceelin + Z United (H?p x 60ml)'),
(454, N'Dung dịch uống Pokemine 50mg Medisun (Hộp 20 ống X 10ml)', N'Dung d?ch u?ng Pokemine 50mg Medisun (H?p 20 ?ng X 10ml)'),
(455, N'Thuốc Enervon United (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Enervon United (H?p 10 V? x 10 Viên)'),
(456, N'Thuốc Myhemo 305mg Reliv (Hộp 3 Vỉ x 10 Viên)', N'Thu?c Myhemo 305mg Reliv (H?p 3 V? x 10 Viên)'),
(457, N'Dung dịch Tot''héma Innothera (Hộp 2 Vỉ x 10 Ống x 10ml)', N'Dung d?ch Tot''héma Innothera (H?p 2 V? x 10 ?ng x 10ml)'),
(458, N'Thuốc Vitamin A-D Hdpharma (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Vitamin A-D Hdpharma (H?p 10 V? x 10 Viên)'),
(459, N'Thuốc Mazzgin DHG Pharma (Hộp 10 Vỉ x 10 Viên)', N'Thu?c Mazzgin DHG Pharma (H?p 10 V? x 10 Viên)'),
(460, N'Viên sủi UPSA-C UPSA SAS (Tuýp 10 Viên)', N'Viên s?i UPSA-C UPSA SAS (Tuýp 10 Viên)')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Name] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Name])=DATALENGTH(s.Original)
 AND (t.[Name] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Name] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Medicines.ActiveIngredient
UPDATE t SET [ActiveIngredient]=s.Original
FROM dbo.[Medicines] t JOIN (VALUES
(298, CAST(N'Vật tư y tế' AS nvarchar(max)), N'V?t t? y t?'),
(300, N'Sản phẩm minh họa', N'S?n ph?m minh h?a'),
(301, N'Vật tư y tế', N'V?t t? y t?'),
(425, N'Eucalyptol (100mg), Tinh dầu tần (0.18mg), Tinh dầu gừng (0.5mg), Menthol natural (0.5mg)', N'Eucalyptol (100mg), Tinh d?u t?n (0.18mg), Tinh d?u g?ng (0.5mg), Menthol natural (0.5mg)'),
(426, N'Húng chanh (2.5g), Núc nác (0.625g), Tinh dầu bạch đàn (6.64mg)', N'Húng chanh (2.5g), Núc nác (0.625g), Tinh d?u b?ch ?àn (6.64mg)'),
(427, N'Húng chanh (45.00g), Núc nác (11.25g), Tinh dầu bạch đàn (119.52mg)', N'Húng chanh (45.00g), Núc nác (11.25g), Tinh d?u b?ch ?àn (119.52mg)'),
(429, N'Cao lá thường xuân (262.5mg), Hoàng liên (Thân rễ) (87.5mg)', N'Cao lá th??ng xuân (262.5mg), Hoàng liên (Thân r?) (87.5mg)'),
(431, N'cineol, Hoàng cầm, Bạch linh, Thiên môn đông, Tang bạch bì, Tiền hồ, Bách bộ, Cát cánh, Tỳ bà lá, Menthol, Cam thảo', N'cineol, Hoàng c?m, B?ch linh, Thiên môn ?ông, Tang b?ch bì, Ti?n h?, Bách b?, Cát cánh, T? bà lá, Menthol, Cam th?o'),
(437, N'Menthol (769mg), Camphor (145mg), Tinh dầu đinh hương (312.5mg), Tinh dầu tràm (39mg), Tinh dầu quế (7.5mg)', N'Menthol (769mg), Camphor (145mg), Tinh d?u ?inh h??ng (312.5mg), Tinh d?u tràm (39mg), Tinh d?u qu? (7.5mg)'),
(438, N'Natri clorid (90mg), Tá dược vừa đủ (10ml)', N'Natri clorid (90mg), Tá d??c v?a ?? (10ml)'),
(441, N'Hoàng liên, Mộc hương, Bạch truật, Bạch thược, Ngũ bội tử, Hậu phác, Cam thảo, Xa tiền tử, Hoạt thạch', N'Hoàng liên, M?c h??ng, B?ch tru?t, B?ch th??c, Ng? b?i t?, H?u phác, Cam th?o, Xa ti?n t?, Ho?t th?ch'),
(442, N'Diệp hạ châu (1.2g), Đảng Sâm (1.2g), Bạch truật (0.6g), Cam thảo (0.6g), Phục Linh (0.6g), Nhân trần (1.2g), Trần bì (0.6g)', N'Di?p h? châu (1.2g), ??ng Sâm (1.2g), B?ch tru?t (0.6g), Cam th?o (0.6g), Ph?c Linh (0.6g), Nhân tr?n (1.2g), Tr?n bì (0.6g)'),
(444, N'Mật heo (60mg), Trần bì (139mg), Nghệ (540.35mg)', N'M?t heo (60mg), Tr?n bì (139mg), Ngh? (540.35mg)'),
(451, N'Vitamin B1, Vitamin B2, Vitamin B6, Vitamin B12, Vitamin B3, Vitamin B5, Magnesia, Vitamin C, Calcium, Vitamin B8, Kẽm', N'Vitamin B1, Vitamin B2, Vitamin B6, Vitamin B12, Vitamin B3, Vitamin B5, Magnesia, Vitamin C, Calcium, Vitamin B8, K?m'),
(452, N'Kẽm (10mg)', N'K?m (10mg)'),
(453, N'Kẽm (10mg), Vitamin C (100mg)', N'K?m (10mg), Vitamin C (100mg)'),
(454, N'Sắt (50mg)', N'S?t (50mg)'),
(456, N'Acid folic (0.35mg), Sắt (100mg)', N'Acid folic (0.35mg), S?t (100mg)'),
(457, N'Mangan gluconat (1.33mg), Đồng gluconat (0.7mg), Sắt gluconat (50mg)', N'Mangan gluconat (1.33mg), ??ng gluconat (0.7mg), S?t gluconat (50mg)')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[ActiveIngredient] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[ActiveIngredient])=DATALENGTH(s.Original)
 AND (t.[ActiveIngredient] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[ActiveIngredient] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Medicines.Unit
UPDATE t SET [Unit]=s.Original
FROM dbo.[Medicines] t JOIN (VALUES
(1, CAST(N'Hộp' AS nvarchar(max)), N'H?p'),
(3, N'Hộp', N'H?p'),
(243, N'Hộp', N'H?p'),
(244, N'Hộp', N'H?p'),
(294, N'Vỉ', N'V?'),
(295, N'Hộp', N'H?p'),
(296, N'Hộp', N'H?p'),
(301, N'Hộp', N'H?p'),
(302, N'Hộp', N'H?p'),
(303, N'Hộp', N'H?p'),
(304, N'Hộp', N'H?p'),
(305, N'Hộp', N'H?p'),
(306, N'Hộp', N'H?p'),
(307, N'Hộp', N'H?p'),
(308, N'Hộp', N'H?p'),
(309, N'Hộp', N'H?p'),
(360, N'Hộp', N'H?p'),
(361, N'Hộp', N'H?p'),
(362, N'Hộp', N'H?p'),
(363, N'Hộp', N'H?p'),
(364, N'Hộp', N'H?p'),
(365, N'Hộp', N'H?p'),
(366, N'Hộp', N'H?p'),
(367, N'Hộp', N'H?p'),
(368, N'Hộp', N'H?p'),
(369, N'Hộp', N'H?p'),
(370, N'Hộp', N'H?p'),
(371, N'Hộp', N'H?p'),
(372, N'Hộp', N'H?p'),
(373, N'Hộp', N'H?p'),
(374, N'Hộp', N'H?p'),
(375, N'Hộp', N'H?p'),
(376, N'Hộp', N'H?p'),
(377, N'Hộp', N'H?p'),
(378, N'Hộp', N'H?p'),
(379, N'Hộp', N'H?p'),
(380, N'Hộp', N'H?p'),
(381, N'Hộp', N'H?p'),
(382, N'Hộp', N'H?p'),
(383, N'Hộp', N'H?p'),
(384, N'Hộp', N'H?p'),
(385, N'Hộp', N'H?p'),
(386, N'Hộp', N'H?p'),
(387, N'Hộp', N'H?p'),
(388, N'Hộp', N'H?p'),
(389, N'Hộp', N'H?p'),
(390, N'Hộp', N'H?p'),
(391, N'Hộp', N'H?p'),
(392, N'Hộp', N'H?p'),
(393, N'Hộp', N'H?p'),
(394, N'Hộp', N'H?p'),
(395, N'Hộp', N'H?p'),
(396, N'Hộp', N'H?p'),
(397, N'Hộp', N'H?p'),
(398, N'Hộp', N'H?p'),
(399, N'Hộp', N'H?p'),
(400, N'Hộp', N'H?p'),
(401, N'Hộp', N'H?p'),
(402, N'Hộp', N'H?p'),
(403, N'Hộp', N'H?p'),
(404, N'Hộp', N'H?p'),
(405, N'Hộp', N'H?p'),
(406, N'Hộp', N'H?p'),
(407, N'Hộp', N'H?p'),
(408, N'Hộp', N'H?p'),
(409, N'Hộp', N'H?p'),
(415, N'Hộp', N'H?p'),
(416, N'Hộp', N'H?p'),
(422, N'Hộp', N'H?p'),
(424, N'Hộp', N'H?p'),
(428, N'Hộp', N'H?p'),
(432, N'Hộp', N'H?p'),
(433, N'Hộp', N'H?p'),
(434, N'Hộp', N'H?p'),
(437, N'Ống', N'?ng'),
(439, N'Hộp', N'H?p'),
(441, N'Hộp', N'H?p'),
(442, N'Hộp', N'H?p'),
(444, N'Hộp', N'H?p'),
(448, N'Hộp', N'H?p'),
(452, N'Hộp', N'H?p'),
(453, N'Hộp', N'H?p'),
(454, N'Hộp', N'H?p'),
(457, N'Hộp', N'H?p')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Unit] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Unit])=DATALENGTH(s.Original)
 AND (t.[Unit] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Unit] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Medicines.PackagingSpec
UPDATE t SET [PackagingSpec]=s.Original
FROM dbo.[Medicines] t JOIN (VALUES
(1, CAST(N'Hộp 15 Vỉ x 12 Viên' AS nvarchar(max)), N'H?p 15 V? x 12 Viên'),
(2, N'Hộp 24 gói x 1g', N'H?p 24 gói x 1g'),
(3, N'Hộp 2 Vỉ x 7 Viên', N'H?p 2 V? x 7 Viên'),
(243, N'Hộp 15 Vỉ x 12 Viên', N'H?p 15 V? x 12 Viên'),
(244, N'Hộp 3 Vỉ x 4 Viên', N'H?p 3 V? x 4 Viên'),
(413, N'Hộp 10 Vỉ x 20 Viên', N'H?p 10 V? x 20 Viên'),
(414, N'Hộp 1 Vỉ x 10 Viên', N'H?p 1 V? x 10 Viên'),
(415, N'Hộp 2 vỉ x 10 viên', N'H?p 2 v? x 10 viên'),
(416, N'Hộp 3 Vỉ x 10 Viên', N'H?p 3 V? x 10 Viên'),
(417, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(418, N'Hộp 10 Vỉ x 20 Viên', N'H?p 10 V? x 20 Viên'),
(419, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(420, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(421, N'Hộp 3 Vỉ x 10 Viên', N'H?p 3 V? x 10 Viên'),
(422, N'Hộp 25 Vỉ x 4 Viên', N'H?p 25 V? x 4 Viên'),
(423, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(424, N'Hộp 4 Vỉ x 4 Viên', N'H?p 4 V? x 4 Viên'),
(425, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(426, N'Hộp 30 Gói x 5ml', N'H?p 30 Gói x 5ml'),
(428, N'Hộp x 60ml', N'H?p x 60ml'),
(429, N'Hộp 9 Gói x 10ml', N'H?p 9 Gói x 10ml'),
(432, N'Hộp 2 Vỉ x 12 Viên', N'H?p 2 V? x 12 Viên'),
(433, N'Hộp X 60ml', N'H?p X 60ml'),
(434, N'Hộp x 10ml', N'H?p x 10ml'),
(437, N'Ống x 1.5g', N'?ng x 1.5g'),
(438, N'Lốc 10 Chai x 10ml', N'L?c 10 Chai x 10ml'),
(439, N'Hộp 1 Lọ X 10ml', N'H?p 1 L? X 10ml'),
(440, N'Hộp 20 Gói X 10ml', N'H?p 20 Gói X 10ml'),
(441, N'Hộp 3 Vỉ x 10 Viên', N'H?p 3 V? x 10 Viên'),
(442, N'Hộp 3 Vỉ x 10 Viên', N'H?p 3 V? x 10 Viên'),
(443, N'Hộp 24 Gói X 10ml', N'H?p 24 Gói X 10ml'),
(444, N'Hộp 5 Vỉ x 10 Viên', N'H?p 5 V? x 10 Viên'),
(445, N'Hộp 30 Gói x 5g', N'H?p 30 Gói x 5g'),
(446, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(447, N'Hộp 20 Gói x 15ml', N'H?p 20 Gói x 15ml'),
(448, N'Hộp 10 Gói', N'H?p 10 Gói'),
(449, N'Hộp 30 Gói x 3g', N'H?p 30 Gói x 3g'),
(450, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(452, N'Hộp 10 vỉ x 10 viên', N'H?p 10 v? x 10 viên'),
(453, N'Hộp x 60ml', N'H?p x 60ml'),
(454, N'Hộp 20 ống X 10ml', N'H?p 20 ?ng X 10ml'),
(455, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(456, N'Hộp 3 Vỉ x 10 Viên', N'H?p 3 V? x 10 Viên'),
(457, N'Hộp 2 Vỉ x 10 Ống x 10ml', N'H?p 2 V? x 10 ?ng x 10ml'),
(458, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên'),
(459, N'Hộp 10 Vỉ x 10 Viên', N'H?p 10 V? x 10 Viên')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[PackagingSpec] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[PackagingSpec])=DATALENGTH(s.Original)
 AND (t.[PackagingSpec] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[PackagingSpec] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Medicines.DescriptionHtml
UPDATE t SET [DescriptionHtml]=s.Original
FROM dbo.[Medicines] t JOIN (VALUES
(1, CAST(N'<p><strong>Thành phần tham khảo:</strong> 1 viên chứa: Caffeine (65mg),, Paracetamol (500mg)</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn: <a href="https://nhathuoclongchau.com.vn/thuoc/panadol-extra-do-500mg-180v-sanofi-16965.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>' AS nvarchar(max)), N'<p><strong>Thành ph?n tham kh?o:</strong> 1 viên ch?a: Caffeine (65mg),, Paracetamol (500mg)</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n: <a href="https://nhathuoclongchau.com.vn/thuoc/panadol-extra-do-500mg-180v-sanofi-16965.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(2, N'<p><strong>Thành phần tham khảo:</strong> Paracetamol 250mg/gói</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn: <a href="https://nhathuoclongchau.com.vn/thuoc/hapacol-250mg-3530.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n tham kh?o:</strong> Paracetamol 250mg/gói</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n: <a href="https://nhathuoclongchau.com.vn/thuoc/hapacol-250mg-3530.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(3, N'<p><strong>Thành phần tham khảo:</strong> 1 viên chứa: Amoxicillin (875mg), Clavulanic acid (125mg)</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn: <a href="https://nhathuoclongchau.com.vn/thuoc/augmentin-1g-4635.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n tham kh?o:</strong> 1 viên ch?a: Amoxicillin (875mg), Clavulanic acid (125mg)</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n: <a href="https://nhathuoclongchau.com.vn/thuoc/augmentin-1g-4635.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(243, N'<p><strong>Thành phần tham khảo:</strong> 1 viên chứa: Acetaminophen (500mg), Caffeine (25mg), Phenylephrine (5mg)</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn: <a href="https://nhathuoclongchau.com.vn/thuoc/panadol-cam-cum-gsk-1445.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n tham kh?o:</strong> 1 viên ch?a: Acetaminophen (500mg), Caffeine (25mg), Phenylephrine (5mg)</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n: <a href="https://nhathuoclongchau.com.vn/thuoc/panadol-cam-cum-gsk-1445.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(244, N'<p><strong>Thành phần tham khảo:</strong> 1 viên chứa: Amoxicillin (500mg), Clavulanic acid (125mg)</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn: <a href="https://nhathuoclongchau.com.vn/thuoc/klamentin-625mg-3333.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n tham kh?o:</strong> 1 viên ch?a: Amoxicillin (500mg), Clavulanic acid (125mg)</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n: <a href="https://nhathuoclongchau.com.vn/thuoc/klamentin-625mg-3333.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(411, N'<p><strong>Thành phần:</strong> 1 tuýp chứa: Adapalene (0.1%)</p><p><strong>Quy cách:</strong> Tuýp X 30g</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/differin-0130mg-17372.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 tuýp ch?a: Adapalene (0.1%)</p><p><strong>Quy cách:</strong> Tuýp X 30g</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/differin-0130mg-17372.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(412, N'<p><strong>Thành phần:</strong> 1 tuýp chứa: Adapalene (0.1%)</p><p><strong>Quy cách:</strong> Tuýp X 15g</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/klenzit-15g-3330.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 tuýp ch?a: Adapalene (0.1%)</p><p><strong>Quy cách:</strong> Tuýp X 15g</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/klenzit-15g-3330.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(413, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Clorpheniramin maleat (4mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 20 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/clorpheniramin-4-200v-4228.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Clorpheniramin maleat (4mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 20 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/clorpheniramin-4-200v-4228.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(414, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Fexofenadin Hydroclorid (180mg)</p><p><strong>Quy cách:</strong> Hộp 1 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/allerphast-180mg-4805.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Fexofenadin Hydroclorid (180mg)</p><p><strong>Quy cách:</strong> H?p 1 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/allerphast-180mg-4805.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(415, N'<p><strong>Thành phần:</strong> 1 viên chứa: Levocetirizine (5mg)</p><p><strong>Quy cách:</strong> Hộp 2 vỉ x 10 viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/histalong-l-5mg-drreddy-2x10-30344.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Levocetirizine (5mg)</p><p><strong>Quy cách:</strong> H?p 2 v? x 10 viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/histalong-l-5mg-drreddy-2x10-30344.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(416, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Fexofenadin Hydroclorid (60mg)</p><p><strong>Quy cách:</strong> Hộp 3 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/exopadin-60-mg-truong-tho-3-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Fexofenadin Hydroclorid (60mg)</p><p><strong>Quy cách:</strong> H?p 3 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/exopadin-60-mg-truong-tho-3-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(417, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Cetirizin (10mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/cetirizin-10mg-truong-tho-10x10-33669.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Cetirizin (10mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/cetirizin-10mg-truong-tho-10x10-33669.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(418, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Clorpheniramin maleat (4mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 20 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/clorpheniramin-4mg-khanh-hoa-10x20-33614.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Clorpheniramin maleat (4mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 20 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/clorpheniramin-4mg-khanh-hoa-10x20-33614.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(419, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Cetirizin (10mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/cetirizin-stada-10mg-10x10-33218.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Cetirizin (10mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/cetirizin-stada-10mg-10x10-33218.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(420, N'<p><strong>Thành phần:</strong> 1 viên chứa: Loratadine (10mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/a-t-loratadin-10-mg-an-thien-10-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Loratadine (10mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/a-t-loratadin-10-mg-an-thien-10-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(421, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Fexofenadin Hydroclorid (180mg)</p><p><strong>Quy cách:</strong> Hộp 3 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/fexofenadine-hydrochloride-180-mg-an-thien-3-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Fexofenadin Hydroclorid (180mg)</p><p><strong>Quy cách:</strong> H?p 3 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/fexofenadine-hydrochloride-180-mg-an-thien-3-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(422, N'<p><strong>Thành phần:</strong> 1 viên chứa: Dimenhydrinate (50mg)</p><p><strong>Quy cách:</strong> Hộp 25 Vỉ x 4 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/vomina-plus-dimenhydrinate-50mg-medipharco-25x4-34965.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Dimenhydrinate (50mg)</p><p><strong>Quy cách:</strong> H?p 25 V? x 4 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/vomina-plus-dimenhydrinate-50mg-medipharco-25x4-34965.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(423, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Fexofenadin Hydroclorid (60mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/fefasdin-60-khapharco-10x10-28831.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Fexofenadin Hydroclorid (60mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/fefasdin-60-khapharco-10x10-28831.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(424, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Paracetamol (500mg)</p><p><strong>Quy cách:</strong> Hộp 4 Vỉ x 4 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/efferalgan-500mg-1620.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Paracetamol (500mg)</p><p><strong>Quy cách:</strong> H?p 4 V? x 4 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/efferalgan-500mg-1620.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(425, N'<p><strong>Thành phần:</strong> 1 viên chứa: Eucalyptol (100mg), Tinh dầu tần (0.18mg), Tinh dầu gừng (0.5mg), Menthol natural (0.5mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/eugica-vien-uong-dieu-tri-ho-cam-cum-490.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Eucalyptol (100mg), Tinh d?u t?n (0.18mg), Tinh d?u g?ng (0.5mg), Menthol natural (0.5mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/eugica-vien-uong-dieu-tri-ho-cam-cum-490.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(426, N'<p><strong>Thành phần:</strong> 5ml chứa:, Húng chanh (2.5g), Núc nác (0.625g), Tinh dầu bạch đàn (6.64mg)</p><p><strong>Quy cách:</strong> Hộp 30 Gói x 5ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/hoastex-s-opc-30-goi-x-5-ml.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 5ml ch?a:, Húng chanh (2.5g), Núc nác (0.625g), Tinh d?u b?ch ?àn (6.64mg)</p><p><strong>Quy cách:</strong> H?p 30 Gói x 5ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/hoastex-s-opc-30-goi-x-5-ml.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(427, N'<p><strong>Thành phần:</strong> Húng chanh (45.00g), Núc nác (11.25g), Tinh dầu bạch đàn (119.52mg)</p><p><strong>Quy cách:</strong> Chai x 90ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/siro-hoastex-s-opc-90-ml.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> Húng chanh (45.00g), Núc nác (11.25g), Tinh d?u b?ch ?àn (119.52mg)</p><p><strong>Quy cách:</strong> Chai x 90ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/siro-hoastex-s-opc-90-ml.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(428, N'<p><strong>Thành phần:</strong> 1ml chứa:, Desloratadin (0.5mg)</p><p><strong>Quy cách:</strong> Hộp x 60ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/deslotid-opv-60-ml-1.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1ml ch?a:, Desloratadin (0.5mg)</p><p><strong>Quy cách:</strong> H?p x 60ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/deslotid-opv-60-ml-1.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(429, N'<p><strong>Thành phần:</strong> 100ml chứa:, Cao lá thường xuân (262.5mg), Hoàng liên (Thân rễ) (87.5mg)</p><p><strong>Quy cách:</strong> Hộp 9 Gói x 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/synatura-syrup-ahngook-9-goi-x-10-ml.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 100ml ch?a:, Cao lá th??ng xuân (262.5mg), Hoàng liên (Thân r?) (87.5mg)</p><p><strong>Quy cách:</strong> H?p 9 Gói x 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/synatura-syrup-ahngook-9-goi-x-10-ml.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(430, N'<p><strong>Thành phần:</strong> cineol (0.08), Húng chanh (45.00)</p><p><strong>Quy cách:</strong> Chai x 90ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/siro-hoastex-giam-ho-hieu-qua-435.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> cineol (0.08), Húng chanh (45.00)</p><p><strong>Quy cách:</strong> Chai x 90ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/siro-hoastex-giam-ho-hieu-qua-435.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(431, N'<p><strong>Thành phần:</strong> 90ml chứa:, cineol (18.00mg), Hoàng cầm (1.80 ), Bạch linh (1.80 ), Thiên môn đông (2.70 ), Tang bạch bì (1.80 ), Tiền hồ (1.80 ), Bách bộ (2.79 ), Cát cánh (1.80 ), Tỳ bà lá (16.20 ), Menthol (18.00mg), Cam thảo</p><p><strong>Quy cách:</strong> Chai x 90ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/thuoc-ho-opc-nguoi-lon-524.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 90ml ch?a:, cineol (18.00mg), Hoàng c?m (1.80 ), B?ch linh (1.80 ), Thiên môn ?ông (2.70 ), Tang b?ch bì (1.80 ), Ti?n h? (1.80 ), Bách b? (2.79 ), Cát cánh (1.80 ), T? bà lá (16.20 ), Menthol (18.00mg), Cam th?o</p><p><strong>Quy cách:</strong> Chai x 90ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/thuoc-ho-opc-nguoi-lon-524.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(432, N'<p><strong>Thành phần:</strong> 1 viên chứa: Ambroxol (15mg)</p><p><strong>Quy cách:</strong> Hộp 2 Vỉ x 12 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/strepsils-throat-irritation-cough-24-vien-1012.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Ambroxol (15mg)</p><p><strong>Quy cách:</strong> H?p 2 V? x 12 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/strepsils-throat-irritation-cough-24-vien-1012.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(433, N'<p><strong>Thành phần:</strong> Ambroxol hydroclorid (0.3% (kl/tt))</p><p><strong>Quy cách:</strong> Hộp X 60ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/ambroxol-danapha-60ml-29574.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> Ambroxol hydroclorid (0.3% (kl/tt))</p><p><strong>Quy cách:</strong> H?p X 60ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/ambroxol-danapha-60ml-29574.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(434, N'<p><strong>Thành phần:</strong> Xylometazoline hydrochloride (0.1%)</p><p><strong>Quy cách:</strong> Hộp x 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/otrera-0-1-an-thien-10-ml.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> Xylometazoline hydrochloride (0.1%)</p><p><strong>Quy cách:</strong> H?p x 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/otrera-0-1-an-thien-10-ml.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(435, N'<p><strong>Thành phần:</strong> 13ml chứa:, Natri chondroitin sulfat (13.0mg), Chlorphenamine (3.9mg), Vitamin B6 (13.0mg), Vitamin E (6.5mg)</p><p><strong>Quy cách:</strong> Chai x 13ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/vrohto-vitamin-1853.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 13ml ch?a:, Natri chondroitin sulfat (13.0mg), Chlorphenamine (3.9mg), Vitamin B6 (13.0mg), Vitamin E (6.5mg)</p><p><strong>Quy cách:</strong> Chai x 13ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/vrohto-vitamin-1853.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(436, N'<p><strong>Thành phần:</strong> 5ml chứa: Cyanocobalamin (1mg)</p><p><strong>Quy cách:</strong> Chai X 5ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/sancoba-2334.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 5ml ch?a: Cyanocobalamin (1mg)</p><p><strong>Quy cách:</strong> Chai X 5ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/sancoba-2334.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(437, N'<p><strong>Thành phần:</strong> Menthol (769mg), Camphor (145mg), Tinh dầu đinh hương (312.5mg), Tinh dầu tràm (39mg), Tinh dầu quế (7.5mg)</p><p><strong>Quy cách:</strong> Ống x 1.5g</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/ong-hit-sao-vang-15g-tri-nghet-mui-so-mui-ho-18334.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> Menthol (769mg), Camphor (145mg), Tinh d?u ?inh h??ng (312.5mg), Tinh d?u tràm (39mg), Tinh d?u qu? (7.5mg)</p><p><strong>Quy cách:</strong> ?ng x 1.5g</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/ong-hit-sao-vang-15g-tri-nghet-mui-so-mui-ho-18334.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(438, N'<p><strong>Thành phần:</strong> 10ml chứa:, Natri clorid (90mg), Tá dược vừa đủ (10ml)</p><p><strong>Quy cách:</strong> Lốc 10 Chai x 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/natri-clorid-09-traphaco-10ml-18444.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 10ml ch?a:, Natri clorid (90mg), Tá d??c v?a ?? (10ml)</p><p><strong>Quy cách:</strong> L?c 10 Chai x 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/natri-clorid-09-traphaco-10ml-18444.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(439, N'<p><strong>Thành phần:</strong> 10ml chứa:, Natri clorid (90mg)</p><p><strong>Quy cách:</strong> Hộp 1 Lọ X 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/natri-clorid-0-9-bidiphar-10-ml.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 10ml ch?a:, Natri clorid (90mg)</p><p><strong>Quy cách:</strong> H?p 1 L? X 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/natri-clorid-0-9-bidiphar-10-ml.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(440, N'<p><strong>Thành phần:</strong> 10 ml chứa:, Nhôm hydroxyd (0.61g), Magnesi hydroxyd (0.8g), Simethicone (0.08g)</p><p><strong>Quy cách:</strong> Hộp 20 Gói X 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/simegaz-plus-opv-20-goi-x-10-ml-mui-bac-ha-1.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 10 ml ch?a:, Nhôm hydroxyd (0.61g), Magnesi hydroxyd (0.8g), Simethicone (0.08g)</p><p><strong>Quy cách:</strong> H?p 20 Gói X 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/simegaz-plus-opv-20-goi-x-10-ml-mui-bac-ha-1.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(441, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Hoàng liên (1.35g), Mộc hương (1.2g), Bạch truật (0.9g), Bạch thược (0.9g), Ngũ bội tử (0.9g), Hậu phác (0.6g), Cam thảo (0.45g), Xa tiền tử (0.45g), Hoạt thạch (0.15g)</p><p><strong>Quy cách:</strong> Hộp 3 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/dai-trang-truong-phuc-3-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Hoàng liên (1.35g), M?c h??ng (1.2g), B?ch tru?t (0.9g), B?ch th??c (0.9g), Ng? b?i t? (0.9g), H?u phác (0.6g), Cam th?o (0.45g), Xa ti?n t? (0.45g), Ho?t th?ch (0.15g)</p><p><strong>Quy cách:</strong> H?p 3 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/dai-trang-truong-phuc-3-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(442, N'<p><strong>Thành phần:</strong> 1 viên chứa: Diệp hạ châu (1.2g), Đảng Sâm (1.2g), Bạch truật (0.6g), Cam thảo (0.6g), Phục Linh (0.6g), Nhân trần (1.2g), Trần bì (0.6g)</p><p><strong>Quy cách:</strong> Hộp 3 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/bo-gan-truong-phuc-3x10-34370.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Di?p h? châu (1.2g), ??ng Sâm (1.2g), B?ch tru?t (0.6g), Cam th?o (0.6g), Ph?c Linh (0.6g), Nhân tr?n (1.2g), Tr?n bì (0.6g)</p><p><strong>Quy cách:</strong> H?p 3 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/bo-gan-truong-phuc-3x10-34370.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(443, N'<p><strong>Thành phần:</strong> 1 gói chứa: Alginic acid (500mg), Sodium bicarbonate (213mg), Calcium Carbonate (325mg)</p><p><strong>Quy cách:</strong> Hộp 24 Gói X 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/gaviscon-dual-action-24-goi-x-10ml-17619.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 gói ch?a: Alginic acid (500mg), Sodium bicarbonate (213mg), Calcium Carbonate (325mg)</p><p><strong>Quy cách:</strong> H?p 24 Gói X 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/gaviscon-dual-action-24-goi-x-10ml-17619.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(444, N'<p><strong>Thành phần:</strong> 1 viên chứa: Mật heo (60mg), Trần bì (139mg), Nghệ (540.35mg)</p><p><strong>Quy cách:</strong> Hộp 5 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/cholapan-18080.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: M?t heo (60mg), Tr?n bì (139mg), Ngh? (540.35mg)</p><p><strong>Quy cách:</strong> H?p 5 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/cholapan-18080.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(445, N'<p><strong>Thành phần:</strong> 1 gói chứa: Sucralfate (1000mg)</p><p><strong>Quy cách:</strong> Hộp 30 Gói x 5g</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/fudophos-1000-mg-phuong-dong-30-goi.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 gói ch?a: Sucralfate (1000mg)</p><p><strong>Quy cách:</strong> H?p 30 Gói x 5g</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/fudophos-1000-mg-phuong-dong-30-goi.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(446, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Loperamid hydroclorid (2mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/loperamid-2-mg-stella-10-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Loperamid hydroclorid (2mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/loperamid-2-mg-stella-10-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(447, N'<p><strong>Thành phần:</strong> 15ml chứa: Lactulose (10g)</p><p><strong>Quy cách:</strong> Hộp 20 Gói x 15ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/duphalac-abbott-20-goi-x-15-ml-goi-dai-mau-moi.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 15ml ch?a: Lactulose (10g)</p><p><strong>Quy cách:</strong> H?p 20 Gói x 15ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/duphalac-abbott-20-goi-x-15-ml-goi-dai-mau-moi.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(448, N'<p><strong>Thành phần:</strong> 1 gói chứa:, Diosmectite (3g)</p><p><strong>Quy cách:</strong> Hộp 10 Gói</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/smecta-orange-and-vanilla-ipsen-10-goi.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 gói ch?a:, Diosmectite (3g)</p><p><strong>Quy cách:</strong> H?p 10 Gói</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/smecta-orange-and-vanilla-ipsen-10-goi.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(449, N'<p><strong>Thành phần:</strong> 1 gói chứa:, Diosmectite (3g)</p><p><strong>Quy cách:</strong> Hộp 30 Gói x 3g</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/smecta-2236.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 gói ch?a:, Diosmectite (3g)</p><p><strong>Quy cách:</strong> H?p 30 Gói x 3g</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/smecta-2236.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(450, N'<p><strong>Thành phần:</strong> 1 viên chứa: Simethicone (30mg), Magnesium hydroxide (233mg), Nhôm hydroxyd (178mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/kremils-s-3315.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Simethicone (30mg), Magnesium hydroxide (233mg), Nhôm hydroxyd (178mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/kremils-s-3315.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(451, N'<p><strong>Thành phần:</strong> 1 viên chứa: Vitamin B1 (15mg), Vitamin B2 (15mg), Vitamin B6 (10mg), Vitamin B12 (0.01mg), Vitamin B3 (50mg), Vitamin B5 (23mg), Magnesia (100mg), Vitamin C (500mg), Calcium (100mg), Vitamin B8 (0.15mg), Kẽm (10mg)</p><p><strong>Quy cách:</strong> Tuýp 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/berocca-1515.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Vitamin B1 (15mg), Vitamin B2 (15mg), Vitamin B6 (10mg), Vitamin B12 (0.01mg), Vitamin B3 (50mg), Vitamin B5 (23mg), Magnesia (100mg), Vitamin C (500mg), Calcium (100mg), Vitamin B8 (0.15mg), K?m (10mg)</p><p><strong>Quy cách:</strong> Tuýp 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/berocca-1515.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(452, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Kẽm (10mg)</p><p><strong>Quy cách:</strong> Hộp 10 vỉ x 10 viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/farzincol-10mg-3786.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, K?m (10mg)</p><p><strong>Quy cách:</strong> H?p 10 v? x 10 viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/farzincol-10mg-3786.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(453, N'<p><strong>Thành phần:</strong> 5ml chứa:, Kẽm (10mg), Vitamin C (100mg)</p><p><strong>Quy cách:</strong> Hộp x 60ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/ceelin-z-united-60-ml.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 5ml ch?a:, K?m (10mg), Vitamin C (100mg)</p><p><strong>Quy cách:</strong> H?p x 60ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/ceelin-z-united-60-ml.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(454, N'<p><strong>Thành phần:</strong> 10ml chứa:, Sắt (50mg)</p><p><strong>Quy cách:</strong> Hộp 20 ống X 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/pokemine-medisun-20x10ml-29296.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 10ml ch?a:, S?t (50mg)</p><p><strong>Quy cách:</strong> H?p 20 ?ng X 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/pokemine-medisun-20x10ml-29296.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(455, N'<p><strong>Thành phần:</strong> 1 viên chứa: Vitamin B12 (5mcg), Vitamin B1 (50mg), Vitamin B2 (20mg), Vitamin B3 (50mg), Vitamin B5 (20mg), Vitamin B6 (5mg), Vitamin C (500mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/enervon-3867.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Vitamin B12 (5mcg), Vitamin B1 (50mg), Vitamin B2 (20mg), Vitamin B3 (50mg), Vitamin B5 (20mg), Vitamin B6 (5mg), Vitamin C (500mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/enervon-3867.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(456, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Acid folic (0.35mg), Sắt (100mg)</p><p><strong>Quy cách:</strong> Hộp 3 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/myhemo-reliv-3-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Acid folic (0.35mg), S?t (100mg)</p><p><strong>Quy cách:</strong> H?p 3 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/myhemo-reliv-3-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(457, N'<p><strong>Thành phần:</strong> Mangan gluconat (1.33mg), Đồng gluconat (0.7mg), Sắt gluconat (50mg)</p><p><strong>Quy cách:</strong> Hộp 2 Vỉ x 10 Ống x 10ml</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/tothema-1952.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> Mangan gluconat (1.33mg), ??ng gluconat (0.7mg), S?t gluconat (50mg)</p><p><strong>Quy cách:</strong> H?p 2 V? x 10 ?ng x 10ml</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/tothema-1952.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(458, N'<p><strong>Thành phần:</strong> 1 viên chứa: Vitamin A (5000iu), Vitamin D3 (400iu)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/vitamin-a-d-hdpharma-10-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Vitamin A (5000iu), Vitamin D3 (400iu)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/vitamin-a-d-hdpharma-10-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(459, N'<p><strong>Thành phần:</strong> 1 viên chứa:, Magnesi lactat dihydrat (470mg), Vitamin B6 (5mg)</p><p><strong>Quy cách:</strong> Hộp 10 Vỉ x 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/mazzgin-dhg-pharma-10-x10.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a:, Magnesi lactat dihydrat (470mg), Vitamin B6 (5mg)</p><p><strong>Quy cách:</strong> H?p 10 V? x 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/mazzgin-dhg-pharma-10-x10.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>'),
(460, N'<p><strong>Thành phần:</strong> 1 viên chứa: Vitamin C (1000mg)</p><p><strong>Quy cách:</strong> Tuýp 10 Viên</p><p>Đọc tờ hướng dẫn sử dụng và hỏi dược sĩ trước khi dùng.</p><p>Nguồn tham khảo: <a href="https://nhathuoclongchau.com.vn/thuoc/upsa-c-1000mg-1878.html">Nhà thuốc Long Châu</a> (07/11/2026).</p>', N'<p><strong>Thành ph?n:</strong> 1 viên ch?a: Vitamin C (1000mg)</p><p><strong>Quy cách:</strong> Tuýp 10 Viên</p><p>??c t? h??ng d?n s? d?ng và h?i d??c s? tr??c khi dùng.</p><p>Ngu?n tham kh?o: <a href="https://nhathuoclongchau.com.vn/thuoc/upsa-c-1000mg-1878.html">Nhà thu?c Long Châu</a> (07/11/2026).</p>')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[DescriptionHtml] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[DescriptionHtml])=DATALENGTH(s.Original)
 AND (t.[DescriptionHtml] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[DescriptionHtml] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- OrderDetails.DosageInstruction
UPDATE t SET [DosageInstruction]=s.Original
FROM dbo.[OrderDetails] t JOIN (VALUES
(845, CAST(N'Hướng dẫn giả định phục vụ demo, không dùng để điều trị.' AS nvarchar(max)), N'H??ng d?n gi? ??nh ph?c v? demo, không dùng ?? ?i?u tr?.'),
(869, N'Hướng dẫn giả định phục vụ demo, không dùng để điều trị.', N'H??ng d?n gi? ??nh ph?c v? demo, không dùng ?? ?i?u tr?.')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[DosageInstruction] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[DosageInstruction])=DATALENGTH(s.Original)
 AND (t.[DosageInstruction] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[DosageInstruction] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- OrderDetails.MedicineNameSnapshot
UPDATE t SET [MedicineNameSnapshot]=s.Original
FROM dbo.[OrderDetails] t JOIN (VALUES
(837, CAST(N'Nước muối 0,9%' AS nvarchar(max)), N'N??c mu?i 0,9%'),
(838, N'Băng gạc', N'B?ng g?c'),
(839, N'Bông y tế', N'Bông y t?'),
(840, N'Gel rửa tay', N'Gel r?a tay'),
(841, N'Khẩu trang', N'Kh?u trang')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[MedicineNameSnapshot])=DATALENGTH(s.Original)
 AND (t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- OrderDetails.MedicineUnitSnapshot
UPDATE t SET [MedicineUnitSnapshot]=s.Original
FROM dbo.[OrderDetails] t JOIN (VALUES
(713, CAST(N'Hộp' AS nvarchar(max)), N'H?p'),
(715, N'Hộp', N'H?p'),
(776, N'Hộp', N'H?p'),
(782, N'Hộp', N'H?p'),
(785, N'Hộp', N'H?p'),
(806, N'Hộp', N'H?p'),
(834, N'Vỉ', N'V?'),
(835, N'Hộp', N'H?p'),
(836, N'Hộp', N'H?p'),
(841, N'Hộp', N'H?p'),
(842, N'Vỉ', N'V?'),
(843, N'Hộp', N'H?p'),
(844, N'Hộp', N'H?p'),
(845, N'Hộp', N'H?p'),
(858, N'Hộp', N'H?p'),
(859, N'Hộp', N'H?p'),
(860, N'Hộp', N'H?p'),
(861, N'Hộp', N'H?p'),
(862, N'Hộp', N'H?p'),
(863, N'Hộp', N'H?p'),
(864, N'Hộp', N'H?p'),
(865, N'Hộp', N'H?p'),
(866, N'Hộp', N'H?p'),
(867, N'Hộp', N'H?p'),
(868, N'Hộp', N'H?p'),
(869, N'Hộp', N'H?p'),
(870, N'Hộp', N'H?p'),
(871, N'Hộp', N'H?p'),
(872, N'Hộp', N'H?p'),
(873, N'Hộp', N'H?p'),
(874, N'Hộp', N'H?p'),
(875, N'Hộp', N'H?p'),
(876, N'Hộp', N'H?p'),
(877, N'Hộp', N'H?p'),
(878, N'Hộp', N'H?p'),
(879, N'Hộp', N'H?p'),
(880, N'Hộp', N'H?p'),
(881, N'Hộp', N'H?p'),
(882, N'Hộp', N'H?p'),
(883, N'Hộp', N'H?p'),
(884, N'Hộp', N'H?p'),
(885, N'Hộp', N'H?p'),
(886, N'Hộp', N'H?p'),
(887, N'Hộp', N'H?p'),
(888, N'Hộp', N'H?p'),
(889, N'Hộp', N'H?p'),
(890, N'Hộp', N'H?p'),
(891, N'Hộp', N'H?p'),
(892, N'Hộp', N'H?p'),
(893, N'Hộp', N'H?p'),
(894, N'Hộp', N'H?p'),
(895, N'Hộp', N'H?p'),
(896, N'Hộp', N'H?p'),
(897, N'Hộp', N'H?p'),
(898, N'Hộp', N'H?p'),
(899, N'Hộp', N'H?p'),
(900, N'Hộp', N'H?p'),
(901, N'Hộp', N'H?p'),
(902, N'Hộp', N'H?p'),
(903, N'Hộp', N'H?p'),
(904, N'Hộp', N'H?p'),
(905, N'Hộp', N'H?p'),
(906, N'Hộp', N'H?p'),
(907, N'Hộp', N'H?p'),
(908, N'Hộp', N'H?p'),
(909, N'Hộp', N'H?p'),
(910, N'Hộp', N'H?p'),
(911, N'Hộp', N'H?p'),
(912, N'Hộp', N'H?p'),
(913, N'Hộp', N'H?p'),
(914, N'Hộp', N'H?p'),
(915, N'Hộp', N'H?p'),
(916, N'Hộp', N'H?p'),
(917, N'Hộp', N'H?p'),
(918, N'Hộp', N'H?p'),
(919, N'Hộp', N'H?p'),
(920, N'Hộp', N'H?p'),
(921, N'Hộp', N'H?p'),
(922, N'Hộp', N'H?p'),
(923, N'Hộp', N'H?p'),
(924, N'Hộp', N'H?p'),
(925, N'Hộp', N'H?p'),
(926, N'Hộp', N'H?p'),
(927, N'Hộp', N'H?p'),
(928, N'Hộp', N'H?p'),
(929, N'Hộp', N'H?p'),
(930, N'Hộp', N'H?p'),
(931, N'Hộp', N'H?p'),
(932, N'Hộp', N'H?p'),
(933, N'Hộp', N'H?p'),
(934, N'Hộp', N'H?p'),
(935, N'Hộp', N'H?p'),
(936, N'Hộp', N'H?p'),
(937, N'Hộp', N'H?p'),
(938, N'Hộp', N'H?p'),
(939, N'Hộp', N'H?p'),
(940, N'Hộp', N'H?p'),
(941, N'Hộp', N'H?p'),
(942, N'Hộp', N'H?p'),
(943, N'Hộp', N'H?p'),
(944, N'Hộp', N'H?p'),
(945, N'Hộp', N'H?p'),
(946, N'Hộp', N'H?p'),
(947, N'Hộp', N'H?p'),
(948, N'Hộp', N'H?p'),
(949, N'Hộp', N'H?p'),
(950, N'Vỉ', N'V?')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[MedicineUnitSnapshot])=DATALENGTH(s.Original)
 AND (t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- OrderPendingItems.MedicineNameSnapshot
UPDATE t SET [MedicineNameSnapshot]=s.Original
FROM dbo.[OrderPendingItems] t JOIN (VALUES
(300, CAST(N'Băng gạc' AS nvarchar(max)), N'B?ng g?c')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[MedicineNameSnapshot])=DATALENGTH(s.Original)
 AND (t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- OrderPendingItems.MedicineUnitSnapshot
UPDATE t SET [MedicineUnitSnapshot]=s.Original
FROM dbo.[OrderPendingItems] t JOIN (VALUES
(301, CAST(N'Hộp' AS nvarchar(max)), N'H?p'),
(302, N'Hộp', N'H?p'),
(303, N'Hộp', N'H?p'),
(304, N'Vỉ', N'V?'),
(305, N'Hộp', N'H?p'),
(306, N'Hộp', N'H?p'),
(307, N'Hộp', N'H?p'),
(308, N'Hộp', N'H?p'),
(309, N'Hộp', N'H?p'),
(310, N'Hộp', N'H?p'),
(311, N'Hộp', N'H?p'),
(312, N'Hộp', N'H?p'),
(313, N'Hộp', N'H?p'),
(314, N'Hộp', N'H?p'),
(315, N'Hộp', N'H?p')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[MedicineUnitSnapshot])=DATALENGTH(s.Original)
 AND (t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- OrderPendingItems.DosageInstruction
UPDATE t SET [DosageInstruction]=s.Original
FROM dbo.[OrderPendingItems] t JOIN (VALUES
(302, CAST(N'Hướng dẫn giả định phục vụ demo, không dùng để điều trị.' AS nvarchar(max)), N'H??ng d?n gi? ??nh ph?c v? demo, không dùng ?? ?i?u tr?.'),
(312, N'Hướng dẫn giả định phục vụ demo, không dùng để điều trị.', N'H??ng d?n gi? ??nh ph?c v? demo, không dùng ?? ?i?u tr?.'),
(313, N'Hướng dẫn giả định phục vụ demo, không dùng để điều trị.', N'H??ng d?n gi? ??nh ph?c v? demo, không dùng ?? ?i?u tr?.')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[DosageInstruction] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[DosageInstruction])=DATALENGTH(s.Original)
 AND (t.[DosageInstruction] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[DosageInstruction] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Orders.PaymentMethod
UPDATE t SET [PaymentMethod]=s.Original
FROM dbo.[Orders] t JOIN (VALUES
(25, CAST(N'Chưa xác định' AS nvarchar(max)), N'Ch?a xác ??nh'),
(144, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(145, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(173, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(437, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(568, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(619, N'Tiền mặt', N'Ti?n m?t'),
(743, N'Tiền mặt', N'Ti?n m?t'),
(748, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(749, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(765, N'Tiền mặt', N'Ti?n m?t'),
(804, N'Tiền mặt', N'Ti?n m?t'),
(805, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(806, N'Tiền mặt', N'Ti?n m?t'),
(807, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(808, N'Tiền mặt', N'Ti?n m?t'),
(809, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(810, N'Tiền mặt', N'Ti?n m?t'),
(811, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(812, N'Tiền mặt', N'Ti?n m?t'),
(813, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(814, N'Tiền mặt', N'Ti?n m?t'),
(815, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(816, N'Chưa xác định', N'Ch?a xác ??nh'),
(817, N'Chưa xác định', N'Ch?a xác ??nh'),
(818, N'Chưa xác định', N'Ch?a xác ??nh'),
(819, N'Chưa xác định', N'Ch?a xác ??nh'),
(820, N'Chưa xác định', N'Ch?a xác ??nh'),
(821, N'Chưa xác định', N'Ch?a xác ??nh'),
(835, N'Tiền mặt', N'Ti?n m?t'),
(836, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(837, N'Tiền mặt', N'Ti?n m?t'),
(838, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(839, N'Tiền mặt', N'Ti?n m?t'),
(840, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(841, N'Tiền mặt', N'Ti?n m?t'),
(842, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(843, N'Tiền mặt', N'Ti?n m?t'),
(844, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(845, N'Tiền mặt', N'Ti?n m?t'),
(846, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(847, N'Tiền mặt', N'Ti?n m?t'),
(848, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(849, N'Tiền mặt', N'Ti?n m?t'),
(850, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(851, N'Tiền mặt', N'Ti?n m?t'),
(852, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(853, N'Tiền mặt', N'Ti?n m?t'),
(854, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(855, N'Tiền mặt', N'Ti?n m?t'),
(856, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(857, N'Tiền mặt', N'Ti?n m?t'),
(858, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(859, N'Tiền mặt', N'Ti?n m?t'),
(860, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(861, N'Tiền mặt', N'Ti?n m?t'),
(862, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(863, N'Tiền mặt', N'Ti?n m?t'),
(864, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(865, N'Tiền mặt', N'Ti?n m?t'),
(866, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(867, N'Tiền mặt', N'Ti?n m?t'),
(868, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(869, N'Tiền mặt', N'Ti?n m?t'),
(870, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(871, N'Tiền mặt', N'Ti?n m?t'),
(872, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(873, N'Tiền mặt', N'Ti?n m?t'),
(874, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(875, N'Tiền mặt', N'Ti?n m?t'),
(876, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(877, N'Tiền mặt', N'Ti?n m?t'),
(878, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(879, N'Tiền mặt', N'Ti?n m?t'),
(880, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(881, N'Tiền mặt', N'Ti?n m?t'),
(882, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(883, N'Tiền mặt', N'Ti?n m?t'),
(884, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(885, N'Tiền mặt', N'Ti?n m?t'),
(886, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(887, N'Tiền mặt', N'Ti?n m?t'),
(888, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(889, N'Tiền mặt', N'Ti?n m?t'),
(890, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(891, N'Tiền mặt', N'Ti?n m?t'),
(892, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(893, N'Tiền mặt', N'Ti?n m?t'),
(894, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(895, N'Tiền mặt', N'Ti?n m?t'),
(896, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(897, N'Tiền mặt', N'Ti?n m?t'),
(898, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(899, N'Tiền mặt', N'Ti?n m?t'),
(900, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(901, N'Tiền mặt', N'Ti?n m?t'),
(902, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(903, N'Tiền mặt', N'Ti?n m?t'),
(904, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(905, N'Tiền mặt', N'Ti?n m?t'),
(906, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(907, N'Tiền mặt', N'Ti?n m?t'),
(908, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(909, N'Tiền mặt', N'Ti?n m?t'),
(910, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(911, N'Tiền mặt', N'Ti?n m?t'),
(912, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(913, N'Tiền mặt', N'Ti?n m?t'),
(914, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(915, N'Tiền mặt', N'Ti?n m?t'),
(916, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(917, N'Tiền mặt', N'Ti?n m?t'),
(918, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(919, N'Tiền mặt', N'Ti?n m?t'),
(920, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(921, N'Tiền mặt', N'Ti?n m?t'),
(922, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(923, N'Tiền mặt', N'Ti?n m?t'),
(924, N'Chuyển khoản QR', N'Chuy?n kho?n QR'),
(925, N'Chưa xác định', N'Ch?a xác ??nh'),
(926, N'Chưa xác định', N'Ch?a xác ??nh'),
(927, N'Chưa xác định', N'Ch?a xác ??nh'),
(928, N'Chưa xác định', N'Ch?a xác ??nh'),
(929, N'Chưa xác định', N'Ch?a xác ??nh'),
(930, N'Chưa xác định', N'Ch?a xác ??nh'),
(931, N'Chưa xác định', N'Ch?a xác ??nh'),
(932, N'Chưa xác định', N'Ch?a xác ??nh'),
(933, N'Chưa xác định', N'Ch?a xác ??nh'),
(934, N'Chưa xác định', N'Ch?a xác ??nh'),
(935, N'Tiền mặt', N'Ti?n m?t')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[PaymentMethod] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[PaymentMethod])=DATALENGTH(s.Original)
 AND (t.[PaymentMethod] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[PaymentMethod] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Orders.CancelReason
UPDATE t SET [CancelReason]=s.Original
FROM dbo.[Orders] t JOIN (VALUES
(821, CAST(N'Hủy đơn mẫu' AS nvarchar(max)), N'H?y ??n m?u'),
(934, N'Hủy đơn mẫu', N'H?y ??n m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[CancelReason] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[CancelReason])=DATALENGTH(s.Original)
 AND (t.[CancelReason] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[CancelReason] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Orders.PatientName
UPDATE t SET [PatientName]=s.Original
FROM dbo.[Orders] t JOIN (VALUES
(765, CAST(N'Người bệnh mẫu' AS nvarchar(max)), N'Ng??i b?nh m?u'),
(815, N'Người bệnh mẫu', N'Ng??i b?nh m?u'),
(817, N'Người bệnh mẫu', N'Ng??i b?nh m?u'),
(818, N'Người bệnh mẫu', N'Ng??i b?nh m?u'),
(846, N'Người bệnh mẫu', N'Ng??i b?nh m?u'),
(930, N'Người bệnh mẫu', N'Ng??i b?nh m?u'),
(931, N'Người bệnh mẫu', N'Ng??i b?nh m?u'),
(932, N'Người bệnh mẫu', N'Ng??i b?nh m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[PatientName] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[PatientName])=DATALENGTH(s.Original)
 AND (t.[PatientName] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[PatientName] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Orders.Prescriber
UPDATE t SET [Prescriber]=s.Original
FROM dbo.[Orders] t JOIN (VALUES
(765, CAST(N'Bác sĩ mẫu' AS nvarchar(max)), N'Bác s? m?u'),
(815, N'Bác sĩ mẫu', N'Bác s? m?u'),
(818, N'Bác sĩ mẫu', N'Bác s? m?u'),
(846, N'Bác sĩ mẫu', N'Bác s? m?u'),
(931, N'Bác sĩ mẫu', N'Bác s? m?u'),
(932, N'Bác sĩ mẫu', N'Bác s? m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Prescriber] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Prescriber])=DATALENGTH(s.Original)
 AND (t.[Prescriber] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Prescriber] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- StockInDetails.MedicineNameSnapshot
UPDATE t SET [MedicineNameSnapshot]=s.Original
FROM dbo.[StockInDetails] t JOIN (VALUES
(478, CAST(N'Nước muối 0,9%' AS nvarchar(max)), N'N??c mu?i 0,9%'),
(480, N'Băng gạc', N'B?ng g?c'),
(481, N'Bông y tế', N'Bông y t?'),
(482, N'Gel rửa tay', N'Gel r?a tay'),
(483, N'Khẩu trang', N'Kh?u trang')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[MedicineNameSnapshot])=DATALENGTH(s.Original)
 AND (t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[MedicineNameSnapshot] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- StockInDetails.MedicineUnitSnapshot
UPDATE t SET [MedicineUnitSnapshot]=s.Original
FROM dbo.[StockInDetails] t JOIN (VALUES
(475, CAST(N'Vỉ' AS nvarchar(max)), N'V?'),
(476, N'Hộp', N'H?p'),
(477, N'Hộp', N'H?p'),
(479, N'Vỉ', N'V?'),
(483, N'Hộp', N'H?p'),
(484, N'Hộp', N'H?p'),
(485, N'Hộp', N'H?p'),
(486, N'Hộp', N'H?p'),
(487, N'Hộp', N'H?p'),
(488, N'Hộp', N'H?p'),
(489, N'Hộp', N'H?p'),
(490, N'Hộp', N'H?p'),
(591, N'Hộp', N'H?p'),
(592, N'Hộp', N'H?p'),
(593, N'Hộp', N'H?p'),
(594, N'Hộp', N'H?p'),
(595, N'Hộp', N'H?p'),
(596, N'Hộp', N'H?p'),
(597, N'Hộp', N'H?p'),
(598, N'Hộp', N'H?p'),
(599, N'Hộp', N'H?p'),
(600, N'Hộp', N'H?p'),
(601, N'Hộp', N'H?p'),
(602, N'Hộp', N'H?p'),
(603, N'Hộp', N'H?p'),
(604, N'Hộp', N'H?p'),
(605, N'Hộp', N'H?p'),
(606, N'Hộp', N'H?p'),
(607, N'Hộp', N'H?p'),
(608, N'Hộp', N'H?p'),
(609, N'Hộp', N'H?p'),
(610, N'Hộp', N'H?p'),
(611, N'Hộp', N'H?p'),
(612, N'Hộp', N'H?p'),
(613, N'Hộp', N'H?p'),
(614, N'Hộp', N'H?p'),
(615, N'Hộp', N'H?p'),
(616, N'Hộp', N'H?p'),
(617, N'Hộp', N'H?p'),
(618, N'Hộp', N'H?p'),
(619, N'Hộp', N'H?p'),
(620, N'Hộp', N'H?p'),
(621, N'Hộp', N'H?p'),
(622, N'Hộp', N'H?p'),
(623, N'Hộp', N'H?p'),
(624, N'Hộp', N'H?p'),
(625, N'Hộp', N'H?p'),
(626, N'Hộp', N'H?p'),
(627, N'Hộp', N'H?p'),
(628, N'Hộp', N'H?p'),
(629, N'Hộp', N'H?p'),
(630, N'Hộp', N'H?p'),
(631, N'Hộp', N'H?p'),
(632, N'Hộp', N'H?p'),
(633, N'Hộp', N'H?p'),
(634, N'Hộp', N'H?p'),
(635, N'Hộp', N'H?p'),
(636, N'Hộp', N'H?p'),
(637, N'Hộp', N'H?p'),
(638, N'Hộp', N'H?p'),
(639, N'Hộp', N'H?p'),
(640, N'Hộp', N'H?p'),
(641, N'Hộp', N'H?p'),
(642, N'Hộp', N'H?p'),
(643, N'Hộp', N'H?p'),
(644, N'Hộp', N'H?p'),
(645, N'Hộp', N'H?p'),
(646, N'Hộp', N'H?p'),
(647, N'Hộp', N'H?p'),
(648, N'Hộp', N'H?p'),
(649, N'Hộp', N'H?p'),
(650, N'Hộp', N'H?p'),
(651, N'Hộp', N'H?p'),
(652, N'Hộp', N'H?p'),
(653, N'Hộp', N'H?p'),
(654, N'Hộp', N'H?p'),
(655, N'Hộp', N'H?p'),
(656, N'Hộp', N'H?p'),
(657, N'Hộp', N'H?p'),
(658, N'Hộp', N'H?p'),
(659, N'Hộp', N'H?p'),
(660, N'Hộp', N'H?p'),
(661, N'Hộp', N'H?p'),
(662, N'Hộp', N'H?p'),
(663, N'Hộp', N'H?p'),
(664, N'Hộp', N'H?p'),
(665, N'Hộp', N'H?p'),
(666, N'Hộp', N'H?p'),
(667, N'Hộp', N'H?p'),
(668, N'Hộp', N'H?p'),
(669, N'Hộp', N'H?p'),
(670, N'Hộp', N'H?p'),
(671, N'Hộp', N'H?p'),
(672, N'Hộp', N'H?p'),
(673, N'Hộp', N'H?p'),
(674, N'Hộp', N'H?p'),
(675, N'Hộp', N'H?p'),
(676, N'Hộp', N'H?p'),
(677, N'Hộp', N'H?p'),
(678, N'Hộp', N'H?p'),
(679, N'Hộp', N'H?p'),
(680, N'Hộp', N'H?p'),
(681, N'Hộp', N'H?p'),
(682, N'Hộp', N'H?p'),
(683, N'Hộp', N'H?p'),
(684, N'Hộp', N'H?p'),
(685, N'Hộp', N'H?p'),
(686, N'Hộp', N'H?p'),
(687, N'Hộp', N'H?p'),
(688, N'Hộp', N'H?p'),
(689, N'Hộp', N'H?p'),
(690, N'Hộp', N'H?p')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[MedicineUnitSnapshot])=DATALENGTH(s.Original)
 AND (t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[MedicineUnitSnapshot] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- StockIns.Note
UPDATE t SET [Note]=s.Original
FROM dbo.[StockIns] t JOIN (VALUES
(447, CAST(N'Phiếu nhập mẫu' AS nvarchar(max)), N'Phi?u nh?p m?u'),
(448, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(449, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(450, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(471, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(472, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(473, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(474, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(475, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(476, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(477, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(478, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(479, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(480, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(481, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(482, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(483, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(484, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(485, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(486, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(487, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(488, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(489, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u'),
(490, N'Phiếu nhập mẫu', N'Phi?u nh?p m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Note] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Note])=DATALENGTH(s.Original)
 AND (t.[Note] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Note] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Suppliers.Name
UPDATE t SET [Name]=s.Original
FROM dbo.[Suppliers] t JOIN (VALUES
(1, CAST(N'Dược Hậu Giang (DHG)' AS nvarchar(max)), N'D??c H?u Giang (DHG)'),
(2, N'Sanofi Việt Nam', N'Sanofi Vi?t Nam'),
(3, N'Dược phẩm Boston Việt Nam', N'D??c ph?m Boston Vi?t Nam'),
(248, N'Nhà cung cấp 1', N'Nhà cung c?p 1'),
(249, N'Nhà cung cấp 2', N'Nhà cung c?p 2'),
(250, N'Nhà cung cấp 3', N'Nhà cung c?p 3'),
(251, N'Nhà cung cấp 4', N'Nhà cung c?p 4'),
(262, N'Nhà cung cấp 1', N'Nhà cung c?p 1'),
(263, N'Nhà cung cấp 2', N'Nhà cung c?p 2'),
(264, N'Nhà cung cấp 3', N'Nhà cung c?p 3'),
(265, N'Nhà cung cấp 4', N'Nhà cung c?p 4'),
(266, N'Nhà cung cấp 5', N'Nhà cung c?p 5'),
(267, N'Nhà cung cấp 6', N'Nhà cung c?p 6'),
(268, N'Nhà cung cấp 7', N'Nhà cung c?p 7'),
(269, N'Nhà cung cấp 8', N'Nhà cung c?p 8'),
(270, N'Nhà cung cấp 9', N'Nhà cung c?p 9'),
(271, N'Nhà cung cấp 10', N'Nhà cung c?p 10'),
(272, N'Chưa xác định nhà cung cấp', N'Ch?a xác ??nh nhà cung c?p')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Name] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Name])=DATALENGTH(s.Original)
 AND (t.[Name] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Name] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- Suppliers.Address
UPDATE t SET [Address]=s.Original
FROM dbo.[Suppliers] t JOIN (VALUES
(1, CAST(N'Địa chỉ nhà cung cấp mẫu 1' AS nvarchar(max)), N'??a ch? nhà cung c?p m?u 1'),
(2, N'Địa chỉ nhà cung cấp mẫu 2', N'??a ch? nhà cung c?p m?u 2'),
(3, N'Địa chỉ nhà cung cấp mẫu 3', N'??a ch? nhà cung c?p m?u 3'),
(248, N'Địa chỉ nhà cung cấp mẫu 248', N'??a ch? nhà cung c?p m?u 248'),
(249, N'Địa chỉ nhà cung cấp mẫu 249', N'??a ch? nhà cung c?p m?u 249'),
(250, N'Địa chỉ nhà cung cấp mẫu 250', N'??a ch? nhà cung c?p m?u 250'),
(251, N'Địa chỉ nhà cung cấp mẫu 251', N'??a ch? nhà cung c?p m?u 251'),
(262, N'Địa chỉ nhà cung cấp mẫu 262', N'??a ch? nhà cung c?p m?u 262'),
(263, N'Địa chỉ nhà cung cấp mẫu 263', N'??a ch? nhà cung c?p m?u 263'),
(264, N'Địa chỉ nhà cung cấp mẫu 264', N'??a ch? nhà cung c?p m?u 264'),
(265, N'Địa chỉ nhà cung cấp mẫu 265', N'??a ch? nhà cung c?p m?u 265'),
(266, N'Địa chỉ nhà cung cấp mẫu 266', N'??a ch? nhà cung c?p m?u 266'),
(267, N'Địa chỉ nhà cung cấp mẫu 267', N'??a ch? nhà cung c?p m?u 267'),
(268, N'Địa chỉ nhà cung cấp mẫu 268', N'??a ch? nhà cung c?p m?u 268'),
(269, N'Địa chỉ nhà cung cấp mẫu 269', N'??a ch? nhà cung c?p m?u 269'),
(270, N'Địa chỉ nhà cung cấp mẫu 270', N'??a ch? nhà cung c?p m?u 270'),
(271, N'Địa chỉ nhà cung cấp mẫu 271', N'??a ch? nhà cung c?p m?u 271'),
(272, N'Địa chỉ nhà cung cấp mẫu 272', N'??a ch? nhà cung c?p m?u 272')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Address] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Address])=DATALENGTH(s.Original)
 AND (t.[Address] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Address] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- WebCustomerAddresses.Address
UPDATE t SET [Address]=s.Original
FROM dbo.[WebCustomerAddresses] t JOIN (VALUES
(1, CAST(N'Địa chỉ nhận hàng mẫu' AS nvarchar(max)), N'??a ch? nh?n hàng m?u'),
(2, N'Địa chỉ nhận hàng mẫu', N'??a ch? nh?n hàng m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Address] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Address])=DATALENGTH(s.Original)
 AND (t.[Address] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Address] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- WebOrderItems.Unit
UPDATE t SET [Unit]=s.Original
FROM dbo.[WebOrderItems] t JOIN (VALUES
(2, CAST(N'Hộp' AS nvarchar(max)), N'H?p')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Unit] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Unit])=DATALENGTH(s.Original)
 AND (t.[Unit] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Unit] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- WebOrders.ShippingAddress
UPDATE t SET [ShippingAddress]=s.Original
FROM dbo.[WebOrders] t JOIN (VALUES
(1, CAST(N'Địa chỉ giao hàng mẫu' AS nvarchar(max)), N'??a ch? giao hàng m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[ShippingAddress] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[ShippingAddress])=DATALENGTH(s.Original)
 AND (t.[ShippingAddress] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[ShippingAddress] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- WebOrderStatusHistories.Actor
UPDATE t SET [Actor]=s.Original
FROM dbo.[WebOrderStatusHistories] t JOIN (VALUES
(1, CAST(N'Hệ thống mẫu' AS nvarchar(max)), N'H? th?ng m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Actor] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Actor])=DATALENGTH(s.Original)
 AND (t.[Actor] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Actor] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

-- WebOrderStatusHistories.Text
UPDATE t SET [Text]=s.Original
FROM dbo.[WebOrderStatusHistories] t JOIN (VALUES
(1, CAST(N'Lịch sử đơn hàng mẫu' AS nvarchar(max)), N'L?ch s? ??n hàng m?u')
) s(Id, Original, Broken) ON t.Id=s.Id
WHERE t.[Text] COLLATE Latin1_General_100_BIN2<>s.Original COLLATE Latin1_General_100_BIN2
 AND DATALENGTH(t.[Text])=DATALENGTH(s.Original)
 AND (t.[Text] COLLATE Latin1_General_100_BIN2=s.Broken COLLATE Latin1_General_100_BIN2
 OR t.[Text] COLLATE Latin1_General_100_BIN2=CONVERT(nvarchar(max),CONVERT(varchar(max),s.Original COLLATE SQL_Latin1_General_CP1_CI_AS)) COLLATE Latin1_General_100_BIN2);
SET @Fixed+=@@ROWCOUNT;

IF @Fixed>0 AND OBJECT_ID(N'dbo.SystemConfig',N'U') IS NOT NULL
 UPDATE dbo.SystemConfig SET [Value]=CONVERT(nvarchar(255),COALESCE(TRY_CONVERT(bigint,[Value]),0)+1),UpdatedAt=SYSDATETIME() WHERE [Key]=N'DataVersion';
COMMIT;
SELECT @Fixed AS FixedTextCells;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT>0 ROLLBACK;
 THROW;
END CATCH;