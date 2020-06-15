-- INSERT INTO [User]
-- VALUES
--     (
--         NEWID(), 
--         'Adam',
--         'Nowak',
--         'adam.nowak',
--         'aaaa',
--         GETDATE()
-- )

INSERT INTO Photo 
VALUES (
    NEWID(),
    'bc345f9a-f4e9-4a8f-ab48-6b3b1aee790b',
    '52.4049029',
    '16.9193208',
    'TestPhoto.jpeg',
    'New Photo',
    'This is my amazing photo hehe',
    GETDATE()
)