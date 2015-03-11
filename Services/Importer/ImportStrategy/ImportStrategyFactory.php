<?php
namespace ANSR\Services\Importer\ImportStrategy;

/**
 * Class ImportStrategyFactory
 * @package ANSR\Services\Importer\ImportStrategy
 * @author Ivan Yonkov <ivanynkv@gmail.com>
 */
class ImportStrategyFactory
{
    const TYPE_JSON = 'json';

    /**
     * @param string $type
     * @return ImportStrategy
     * @throws \Exception
     */
    public static function create($type)
    {
        switch (strtolower($type))
        {
            case self::TYPE_JSON:
                return new JSONImportStrategy();
            default:
                throw new \Exception('Invalid import type');
        }
    }
}